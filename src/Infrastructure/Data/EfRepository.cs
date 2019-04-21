namespace RecipeManager.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Interfaces;
    using RecipeManager.ApplicationCore.Paging;
    using RecipeManager.ApplicationCore.Search;
    using RecipeManager.ApplicationCore.Sort;
    using RecipeManager.Infrastructure.Extensions;

    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly RecipeApiDbContext Context;
        
        public EfRepository(RecipeApiDbContext context)
        {
            this.Context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.Context.Set<T>().IncludeAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await this.Context.Set<T>().ToArrayAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(PagingOptions pagingOptions, SortOptions<T> sortOptions, SearchOptions<T> searchOptions)
        {
            IQueryable<T> query = this.Context.Set<T>();
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var items = await query.Skip(pagingOptions.Offset.Value).Take(pagingOptions.Limit.Value).IncludeAll().ToArrayAsync();

            return items;
        }

        public async Task<int> CountAsync(SortOptions<T> sortOptions, SearchOptions<T> searchOptions)
        {
            IQueryable<T> query = this.Context.Set<T>();
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            return await query.CountAsync();
        }
    }
}
