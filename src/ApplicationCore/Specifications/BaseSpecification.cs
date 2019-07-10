using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public List<Expression<Func<T, bool>>> Criteria { get; } = new List<Expression<Func<T, bool>>>();

        public List<(Expression<Func<T, object>>, bool)> OrderBy { get; } = new List<(Expression<Func<T, object>>, bool)>();

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        protected void ApplyPaging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
            this.IsPagingEnabled = true;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression, bool descending = false)
        {
            this.OrderBy.Add((orderByExpression, descending));
        }

        protected void ApplyOptions(SpecificationOptions<T> options)
        {
            new SearchOptionsProcessor<T>(options.Search).Apply(this);
            new SortOptionsProcessor<T>(options.OrderBy).Apply(this);
            if (options.Paging != null)
            {
                this.ApplyPaging(options.Paging.Offset ?? 0, options.Paging.Limit ?? int.MaxValue);
            }
        }
    }
}
