// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSearchExpressionProvider.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the DefaultSearchExpressionProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using System;
    using System.Linq.Expressions;

    using RecipeManager.Web.Extensions;
    using RecipeManager.Web.Models;

    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
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
