using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public List<Expression<Func<T, bool>>> Criteria { get; } = new List<Expression<Func<T, bool>>>();

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        public List<(Expression<Func<T, object>>, bool)> OrderBy { get; } = new List<(Expression<Func<T, object>>, bool)>();

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            this.IncludeStrings.Add(includeString);
        }

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

        protected void ApplySortOptions(SortOptions<T> sortOptions)
        {
            var processor = new SortOptionsProcessor<T>(sortOptions.OrderBy);
            processor.Apply(this);
        }

        protected void ApplySearchOptions(SearchOptions<T> searchOptions)
        {
            var processor = new SearchOptionsProcessor<T>(searchOptions.Search);
            processor.Apply(this);
        }
    }
}
