using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.ApplicationCore.Search
{
    using System;
    using System.Linq.Expressions;

    public class DecimalToIntSearchExpressionProvider : SearchExpressionProvider
    {
        public override ConstantExpression GetValue(string input)
        {
            if (!decimal.TryParse(input, out var dec))
            {
                throw new ArgumentException("Invalid search value");
            }

            var places = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];
            if (places < 2)
            {
                places = 2;
            }

            var justDigits = (int)(dec * (decimal)Math.Pow(10, places));
            return Expression.Constant(justDigits);
        }

        public override ExpressionResult Evaluate(MemberExpression left, string op, ConstantExpression right)
        {
            // TODO: Add contains operator for strings
            switch (op.ToLower())
            {
                case "gt": return new ExpressionResult { ServerSide = Expression.GreaterThan(left, right) };
                case "gte": return new ExpressionResult { ServerSide = Expression.GreaterThanOrEqual(left, right) };
                case "lt": return new ExpressionResult { ServerSide = Expression.LessThan(left, right) };
                case "lte": return new ExpressionResult { ServerSide = Expression.LessThanOrEqual(left, right) };

                // If nothing matches, fall back to base implementation
                default: return base.Evaluate(left, op, right);
            }
        }
    }
}
