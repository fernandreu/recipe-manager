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
            return await this.Context.Set<T>().IncludeAll(true).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await this.Context.Set<T>().IncludeAll(false).ToArrayAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.GetQuery(this.Context.Set<T>().AsQueryable(), spec);
        }    
    }
}
