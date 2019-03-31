// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IngredientExpressionProvider.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the IngredientExpressionProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using RecipeManager.Models;
    
    public class IngredientExpressionProvider : DefaultSearchExpressionProvider
    {
        public override Expression GetComparison(MemberExpression left, string op, ConstantExpression right)
        {
            if (!op.Equals("contains", StringComparison.OrdinalIgnoreCase))
            {
                return base.GetComparison(left, op, right);
            }

            // The expression is: Items.Any(x => x.Name.Contains(right, StringComparison.OrdinalIgnoreCase)), with Items being IEnumerable<IngredientEntity>
            var lambdaVar = Expression.Variable(typeof(IngredientEntity), "x");
            var nameProperty = Expression.Property(lambdaVar, nameof(IngredientEntity.Name));

            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string), typeof(StringComparison) });
            var containsCall = Expression.Call(nameProperty, containsMethod, right, Expression.Constant(StringComparison.OrdinalIgnoreCase));
            
            var whereLambda = Expression.Lambda(containsCall, lambdaVar);
            
            return Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new[] { typeof(IngredientEntity) }, left, whereLambda);
        }
    }
}
