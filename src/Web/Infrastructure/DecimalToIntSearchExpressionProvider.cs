// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecimalToIntSearchExpressionProvider.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the DecimalToIntSearchExpressionProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using System;
    using System.Linq.Expressions;

    public class DecimalToIntSearchExpressionProvider : DefaultSearchExpressionProvider
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

        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            // TODO: Add contains operator for strings
            switch (op.ToLower())
            {
                case "gt": return Expression.GreaterThan(left, right);
                case "gte": return Expression.GreaterThanOrEqual(left, right);
                case "lt": return Expression.LessThan(left, right);
                case "lte": return Expression.LessThanOrEqual(left, right);
                
                // If nothing matches, fall back to base implementation
                default: return base.GetComparison(left, op, right);
            }
        }
    }
}
