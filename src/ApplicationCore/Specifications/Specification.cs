using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    [SuppressMessage("ReSharper", "CA2227")]
    public sealed class Specification<T> where T : ISingleEntity
    {
        public Specification()
        {
        }
        
        public Specification(SpecificationOptions<T> options)
        {
            ApplyOptions(options);
        }
        
        public ICollection<Expression<Func<T, bool>>>? ServerCriteria { get; set; }

        public ICollection<Expression<Func<T, bool>>>? ClientCriteria { get; set; }

        public ICollection<OrderByClause<T>>? OrderByClauses { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPagingEnabled { get; set; }

        public Specification<T> Where(Expression<Func<T, bool>> expression)
        {
            ClientCriteria ??= new List<Expression<Func<T, bool>>>();
            // TODO: Analyze the expression to determine if it could be evaluated server-side
            ClientCriteria.Add(expression);
            return this;
        }

        public Specification<T> OrderBy(Expression<Func<T, object>> expression, bool descending = false)
        {
            var clause = new OrderByClause<T>
            {
                Expression = expression,
                Descending = descending,
            };
            return OrderBy(clause);
        }

        public Specification<T> OrderBy(OrderByClause<T> clause)
        {
            OrderByClauses ??= new List<OrderByClause<T>>();
            OrderByClauses.Add(clause);
            return this;
        }

        public Specification<T> Paging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
            return this;
        }

        private void ApplyOptions(SpecificationOptions<T> options)
        {
            new SearchOptionsProcessor<T>(options?.Search).Apply(this);
            new SortOptionsProcessor<T>(options?.OrderBy).Apply(this);
            if (options?.Paging != null)
            {
                Paging(options.Paging.Offset ?? 0, options.Paging.Limit ?? int.MaxValue);
            }
        }
    }
}
