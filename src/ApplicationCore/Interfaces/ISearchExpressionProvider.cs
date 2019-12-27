using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.ApplicationCore.Interfaces
{
    using System.Linq.Expressions;

    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);

        ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right);
    }
}
