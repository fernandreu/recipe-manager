using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class ServiceBase<TEntity, TResource> : IAsyncService<TEntity, TResource>
        where TEntity : BaseEntity
        where TResource : BaseResource
    {
        private readonly RecipeApiDbContext context;

        private readonly IConfigurationProvider mappingConfiguration;

        public ServiceBase(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            this.context = context;
            this.mappingConfiguration = mappingConfiguration;
        }

        public async Task<TResource> GetByIdAsync(Guid id)
        {
            var entity = await this.context.Set<TEntity>().IncludeAll(true).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            var mapper = this.mappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
        
        public async Task<PagedResults<TResource>> ListAsync(ISpecification<TEntity> spec)
        {
            var query = this.context.Set<TEntity>().IncludeAll(false).With(spec);
            var size = await query.CountAsync();
            var items = await query.ProjectTo<TResource>(this.mappingConfiguration).ToArrayAsync();
            
            return new PagedResults<TResource>
            {
                Items = items,
                TotalSize = size,
            };
        }
    }
}
