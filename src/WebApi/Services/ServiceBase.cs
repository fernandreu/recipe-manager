using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class ServiceBase<TEntity, TResource> : IAsyncService<TResource>
        where TEntity : class, ISingleEntity
        where TResource : BaseResource
    {
        protected AppDbContext Context { get; }

        protected IConfigurationProvider MappingConfiguration { get; }

        public ServiceBase(AppDbContext context, IConfigurationProvider mappingConfiguration)
        {
            Context = context;
            MappingConfiguration = mappingConfiguration;
        }

        public async Task<TResource?> GetByIdAsync(Guid id)
        {
            var entity = await Context.Set<TEntity>()
                .IncludeAll(true)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (entity == null)
            {
                return null;
            }

            var mapper = MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }
        
        public async Task<PagedResults<TResource>> ListAsync(Specification<TResource> spec)
        {
            var mapper = MappingConfiguration.CreateMapper();
            var entities = await Context.Set<TEntity>()
                .IncludeAll(false)
                .ApplyAsync(spec, mapper)
                .ConfigureAwait(false);

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

            var mapper = MappingConfiguration.CreateMapper();
            var entity = mapper.Map<TEntity>(model);
            entity.Id = Guid.NewGuid();
            await Context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
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

            var entity = await Context.Set<TEntity>()
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

            Context.SaveChanges();

            var mapper = MappingConfiguration.CreateMapper();
            return mapper.Map<TResource>(entity);
        }

        public async Task<int> DeleteAllAsync()
        {
            var entities = await Context.Set<TEntity>()
                .IncludeAll(false) // This will remove some foreign key constraint errors
                .ToListAsync()
                .ConfigureAwait(false);
            Context.Set<TEntity>().RemoveRange(entities);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return entities.Count;
        }
    }
}
