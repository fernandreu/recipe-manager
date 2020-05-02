using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Models;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.ApplicationCore.Search
{
    public class IngredientExpressionProvider : SearchExpressionProvider
    {
        public override ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Is(SearchOperator.Contains))
            {
                return base.Evaluate(left, op, right);
            }

            var searchTerm = IngredientSearchTerm.Parse(right?.Value?.ToString());
            
            var result = new ExpressionResult();
            if (searchTerm == null)
            {
                return result;
            }

            result.ServerSide = GenerateContainsExpression(left, searchTerm.Name);

            if (searchTerm.Quantity == null || right == null)
            {
                return result;
            }

            var genericType = left.Type.GetGenericArguments()[0];
            
            result.ClientSide = Expression.Call(
                typeof(IngredientExtensions),
                nameof(IngredientExtensions.IsMatch),
                new[] { genericType }, left, right);

            return result;
        }

        [SuppressMessage("ReSharper", "CA1307", Justification = "Using case-insensitive Contains would make server-side evaluation impossible")]
        [SuppressMessage("ReSharper", "CA1304", Justification = "Using ToUpperInvariant() would make server-side evaluation impossible")]
        private static Expression GenerateContainsExpression(MemberExpression left, string ingredient)
        {
            Expression<Func<IngredientResource, bool>> predicate = x => x.Name.ToUpper().Contains(ingredient.ToUpper());
            var any = typeof(Enumerable).GetMethods().First(x => x.Name == nameof(Enumerable.Any) && x.GetParameters().Length == 2);
            var genericAny = any.MakeGenericMethod(typeof(IngredientResource));

            return Expression.Call(genericAny, left, predicate);
        }
    }
}
