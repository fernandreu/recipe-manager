using System;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Models;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.Infrastructure.Interfaces;

namespace RecipeManager.Infrastructure.Search
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
