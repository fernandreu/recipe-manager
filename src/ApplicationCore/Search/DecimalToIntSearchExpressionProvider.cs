using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.ApplicationCore.Search
{
    public class DecimalToIntSearchExpressionProvider : SearchExpressionProvider
    {
        [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exceptions are not localized")]
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
            if (op == null)
            {
                throw new ArgumentNullException(nameof(op));
            }

            // TODO: Add contains operator for strings
            return op.ToUpperInvariant() switch
            {
                "GT" => new ExpressionResult {ServerSide = Expression.GreaterThan(left, right)},
                "GTE" => new ExpressionResult {ServerSide = Expression.GreaterThanOrEqual(left, right)},
                "LT" => new ExpressionResult {ServerSide = Expression.LessThan(left, right)},
                "LTE" => new ExpressionResult {ServerSide = Expression.LessThanOrEqual(left, right)},
                _ => base.Evaluate(left, op, right)
            };
        }
    }
}
