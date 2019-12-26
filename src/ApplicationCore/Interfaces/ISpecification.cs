using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface ISpecification<T>
    {
        /// <summary>
        /// Any where clause which can be evaluated server-side (e.g. using simple SQL expressions)
        /// </summary>
        ICollection<Expression<Func<T, bool>>> ServerCriteria { get; }

        /// <summary>
        /// Any where clause which cannot be evaluated server-side (e.g. using custom C# functions)
        /// </summary>
        ICollection<Expression<Func<T, bool>>> ClientCriteria { get; }

        ICollection<OrderByClause<T>> OrderByClauses { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get;}
    }
}
