using System;
using System.Linq.Expressions;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class OrderByClause<T>
    {
        public Expression<Func<T, object>>? Expression { get; set; }

        public bool Descending { get; set; }
    }
}
