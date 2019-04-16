// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchExpressionProvider.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the ISearchExpressionProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using System.Linq.Expressions;

    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);

        Expression GetComparison(MemberExpression left, string op, ConstantExpression right);
    }
}
