namespace RecipeManager.ApplicationCore.Search
{
    using System;
    using System.Linq.Expressions;

    using RecipeManager.ApplicationCore.Extensions;
    using RecipeManager.ApplicationCore.Interfaces;

    public class SearchExpressionProvider : ISearchExpressionProvider
    {
        public virtual ConstantExpression GetValue(string input) => Expression.Constant(input);

        public virtual Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Is(SearchOperator.Equal))
            {
                throw new ArgumentException($"Invalid operator '{op}'.");
            }

            return Expression.Equal(left, right);
        }
    }
}
