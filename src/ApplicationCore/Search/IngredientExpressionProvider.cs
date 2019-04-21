namespace RecipeManager.ApplicationCore.Search
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using RecipeManager.ApplicationCore.Extensions;
    using RecipeManager.ApplicationCore.Interfaces;

    public class IngredientExpressionProvider : SearchExpressionProvider
    {
        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Is(SearchOperator.Contains))
            {
                return base.GetComparison(left, op, right);
            }

            // The expression is: Items.IsMatch(right), with Items being IEnumerable<Ingredient>
            // However, IsMatch is an extension method, so in reality the expression is: IngredientExtensions.IsMatch(Items, right)
            var isMatchMethod = typeof(IngredientExtensions).GetMethod(nameof(IngredientExtensions.IsMatch), new[] { typeof(IEnumerable<IIngredient>), typeof(string) });
            return Expression.Call(isMatchMethod, left, right);
        }
    }
}
