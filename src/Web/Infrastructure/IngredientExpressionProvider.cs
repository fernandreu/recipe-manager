// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IngredientExpressionProvider.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the IngredientExpressionProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using RecipeManager.Web.Extensions;
    using RecipeManager.Web.Models;

    public class IngredientExpressionProvider : DefaultSearchExpressionProvider
    {
        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Is(SearchOperator.Contains))
            {
                return base.GetComparison(left, op, right);
            }

            // The expression is: Items.IsMatch(right), with Items being IEnumerable<IngredientEntity>
            // However, IsMatch is an extension method, so in reality the expression is: IngredientExtensions.IsMatch(Items, right)
            var isMatchMethod = typeof(IngredientExtensions).GetMethod(nameof(IngredientExtensions.IsMatch), new[] { typeof(IEnumerable<IIngredient>), typeof(string) });
            return Expression.Call(isMatchMethod, left, right);
        }
    }
}
