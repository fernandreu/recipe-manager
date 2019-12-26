using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public ICollection<Expression<Func<T, bool>>> ServerCriteria { get; } = new List<Expression<Func<T, bool>>>();

        public ICollection<Expression<Func<T, bool>>> ClientCriteria { get; } = new List<Expression<Func<T, bool>>>();

        public ICollection<OrderByClause<T>> OrderByClauses { get; } = new List<OrderByClause<T>>();

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        public Specification<T> Where(Expression<Func<T, bool>> expression)
        {
            // TODO: Analyze the expression to determine if it could be evaluated server-side
            this.ClientCriteria.Add(expression);
            return this;
        }

        public Specification<T> OrderBy(Expression<Func<T, object>> expression, bool descending = false)
        {
            var clause = new OrderByClause<T>(expression, descending);
            return this.OrderBy(clause);
        }

        public Specification<T> OrderBy(OrderByClause<T> clause)
        {
            this.OrderByClauses.Add(clause);
            return this;
        }

        public Specification<T> Paging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
            this.IsPagingEnabled = true;
            return this;
        }

        protected void ApplyOptions(SpecificationOptions<T> options)
        {
            new SearchOptionsProcessor<T>(options.Search).Apply(this);
            new SortOptionsProcessor<T>(options.OrderBy).Apply(this);
            if (options.Paging != null)
            {
                this.Paging(options.Paging.Offset ?? 0, options.Paging.Limit ?? int.MaxValue);
            }
        }
    }
}
