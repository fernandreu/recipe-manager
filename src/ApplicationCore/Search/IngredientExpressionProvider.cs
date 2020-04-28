using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Models;

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

            if (searchTerm.Quantity == null)
            {
                return result;
            }

            // The expression is: Items.IsMatch(right), with Items being IEnumerable<Ingredient>
            // However, IsMatch is an extension method, so in reality the expression is: IngredientExtensions.IsMatch(Items, right)
            var isMatchMethod = typeof(IngredientExtensions).GetMethod(nameof(IngredientExtensions.IsMatch), new[] { typeof(IEnumerable<IIngredient>), typeof(string) });
            if (isMatchMethod != null && right != null)
            {
                result.ClientSide = Expression.Call(isMatchMethod, left, right);
            }

            return result;
        }

        [SuppressMessage("ReSharper", "CA1307", Justification = "Ignoring case would make server-side evaluation impossible")]
        private static Expression GenerateContainsExpression(MemberExpression left, string ingredient)
        {
            Expression<Func<RecipeIngredient, bool>> predicate = x => x.Name.Contains(ingredient);
            var any = typeof(Enumerable).GetMethods().First(x => x.Name == nameof(Enumerable.Any) && x.GetParameters().Length == 2);
            var genericAny = any.MakeGenericMethod(typeof(RecipeIngredient));

            return Expression.Call(genericAny, left, predicate);
        }
    }
}
