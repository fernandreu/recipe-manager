using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.Infrastructure.Interfaces
{
    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);

        ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right);
    }
}
