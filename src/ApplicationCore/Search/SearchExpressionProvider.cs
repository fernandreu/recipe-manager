using System;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.ApplicationCore.Search
{
    public class SearchExpressionProvider : ISearchExpressionProvider
    {
        public virtual ConstantExpression GetValue(string input) => Expression.Constant(input);

        public virtual ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Is(SearchOperator.Equal))
            {
                throw new ArgumentException($"Invalid operator '{op}'.");
            }

            return new ExpressionResult
            {
                ServerSide = Expression.Equal(left, right),
            };
        }
    }
}
