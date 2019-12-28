using System.Linq.Expressions;

namespace RecipeManager.ApplicationCore.Models
{
    public class ExpressionResult
    {
        public Expression? ServerSide { get; set; }

        public Expression? ClientSide { get; set; }
    }
}
