using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);

        ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right);
    }
}
