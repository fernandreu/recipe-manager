using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criteria { get; }

        /// <summary>
        /// The bool parameter is whether OrderBy should be descending or not
        /// </summary>
        List<(Expression<Func<T, object>>, bool)> OrderBy { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get;}
    }
}
