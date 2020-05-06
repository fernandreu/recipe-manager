using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class IngredientService : ServiceBase<UserIngredient, IngredientResource>, IIngredientService
    {
        public IngredientService(AppDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }

        public async Task<PagedResults<IngredientResource>> ListAsync(Specification<IngredientResource> spec, Guid userId)
        {
            var mapper = MappingConfiguration.CreateMapper();
            var entities = await Context.UserIngredients
                .Where(x => x.UserId == userId)
                .IncludeAll(false)
                .ApplyAsync(spec, mapper)
                .ConfigureAwait(false);

            var items = entities.Items
                .Select(x => mapper.Map<IngredientResource>(x))
                .ToArray();
            
            return new PagedResults<IngredientResource>
            {
                Items = items,
                TotalSize = entities.TotalSize,
            };
        }

        public async Task<IEnumerable<IngredientResource>> UpdateAsync(IEnumerable<IngredientResource> ingredients, Guid userId)
        {
            var originalItems = await Context.UserIngredients
                .Where(x => x.UserId == userId)
                .ToListAsync().ConfigureAwait(false);

            var updatedItems = ingredients?.ToList() ?? new List<IngredientResource>();
            
            var mapper = MappingConfiguration.CreateMapper();
            foreach (var original in originalItems)
            {
                var updated = updatedItems.FirstOrDefault(x => x.Id == original.Id);
                if (updated == null)
                {
                    // Delete the original
                    Context.UserIngredients.Remove(original);
                    continue;
                }

                mapper.Map(updated, original);
            }

            foreach (var updated in updatedItems)
            {
                if (updated.Id != default)
                {
                    var original = originalItems.FirstOrDefault(x => x.Id == updated.Id);
                    if (original != null)
                    {
                        continue;
                    }

                    updated.Id = default;
                }
                
                var entity = mapper.Map<UserIngredient>(updated);
                entity.User = null;
                entity.UserId = userId;

                // Sanitize the ID, just in case it was passed incorrectly
                entity.Id = default;

                Context.Add(entity);
                originalItems.Add(entity);
            }

            await Context.SaveChangesAsync().ConfigureAwait(false);
            return originalItems.Select(x => mapper.Map<IngredientResource>(x));
        }
    }
}
