using System;
using System.Linq;
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
        protected readonly RecipeApiDbContext Context;

        protected readonly IConfigurationProvider MappingConfiguration;

        public ServiceBase(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            this.Context = context;
            this.MappingConfiguration = mappingConfiguration;
        }

        public async Task<TResource> GetByIdAsync(Guid id)
        {
            var entity = await this.Context.Set<TEntity>().IncludeAll(true).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            var mapper = this.MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
        
        public async Task<PagedResults<TResource>> ListAsync(ISpecification<TEntity> spec)
        {
            var query = this.Context.Set<TEntity>().IncludeAll(false).With(spec);
            var size = await query.CountAsync();
            var items = await query.ProjectTo<TResource>(this.MappingConfiguration).ToArrayAsync();
            
            return new PagedResults<TResource>
            {
                Items = items,
                TotalSize = size,
            };
        }

        public async Task<TResource> CreateAsync(TResource model)
        {
            var mapper = this.MappingConfiguration.CreateMapper();
            var entity = mapper.Map<TEntity>(model);
            entity.Id = Guid.NewGuid();
            await this.Context.Set<TEntity>().AddAsync(entity);
            model.Id = entity.Id;
            return mapper.Map<TResource>(entity);
        }

        public async Task<TResource> UpdateAsync(Guid id, TResource model)
        {
            var excludedProperties = new[]
            {
                nameof(BaseResource.Id),
                nameof(BaseResource.Href),
                nameof(BaseResource.Relations),
            };

            var entity = await this.Context.Set<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return null;
            }

            foreach (var property in typeof(TResource).GetProperties())
            {
                if (excludedProperties.Contains(property.Name))
                {
                    continue;
                }

                var value = property.GetValue(model);

                if (value == null)
                {
                    continue;
                }

                var entityProperty = typeof(TEntity).GetProperty(property.Name);
                entityProperty.SetValue(entity, value);
            }

            this.Context.SaveChanges();

            var mapper = this.MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
    }
}
