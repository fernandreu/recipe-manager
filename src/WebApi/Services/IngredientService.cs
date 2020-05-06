using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}
