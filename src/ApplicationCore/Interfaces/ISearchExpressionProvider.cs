namespace RecipeManager.ApplicationCore.Interfaces
{
    using System.Linq.Expressions;

    public interface ISearchExpressionProvider
    {
        ConstantExpression GetValue(string input);

        Expression GetComparison(MemberExpression left, string op, ConstantExpression right);
    }
}
