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
        protected ApplicationDbContext Context { get; }

        protected IConfigurationProvider MappingConfiguration { get; }

        public ServiceBase(ApplicationDbContext context, IConfigurationProvider mappingConfiguration)
        {
            this.Context = context;
            this.MappingConfiguration = mappingConfiguration;
        }

        public async Task<TResource?> GetByIdAsync(Guid id)
        {
            var entity = await this.Context.Set<TEntity>()
                .IncludeAll(true)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (entity == null)
            {
                return null;
            }

            var mapper = this.MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
        
        public async Task<PagedResults<TResource>> ListAsync(ISpecification<TEntity> spec)
        {
            var entities = await this.Context.Set<TEntity>()
                .IncludeAll(false)
                .ApplyAsync(spec)
                .ConfigureAwait(false);

            var mapper = this.MappingConfiguration.CreateMapper();
            var items = entities.Items.Select(x => mapper.Map<TResource>(x)).ToArray();
            
            return new PagedResults<TResource>
            {
                Items = items,
                TotalSize = entities.TotalSize,
            };
        }

        public async Task<TResource> CreateAsync(TResource model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var mapper = this.MappingConfiguration.CreateMapper();
            var entity = mapper.Map<TEntity>(model);
            entity.Id = Guid.NewGuid();
            await this.Context.Set<TEntity>().AddAsync(entity);
            model.Id = entity.Id;
            return mapper.Map<TResource>(entity);
        }

        public async Task<TResource?> UpdateAsync(Guid id, TResource model)
        {
            var excludedProperties = new[]
            {
                nameof(BaseResource.Id),
                nameof(BaseResource.Href),
                nameof(BaseResource.Relations),
            };

            var entity = await this.Context.Set<TEntity>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
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
                entityProperty?.SetValue(entity, value);
            }

            this.Context.SaveChanges();

            var mapper = this.MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
    }
}
