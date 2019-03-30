// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRecipeService.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the DefaultRecipeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using RecipeManager.Models;

    public class DefaultRecipeService : IRecipeService
    {
        private readonly RecipeApiDbContext context;
        
        private readonly IConfigurationProvider mappingConfiguration;

        public DefaultRecipeService(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            this.context = context;
            this.mappingConfiguration = mappingConfiguration;
        }

        public async Task<Recipe> GetRecipeAsync(Guid id)
        {
            var entity = await this.context.Recipes.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            var mapper = this.mappingConfiguration.CreateMapper();
            return mapper.Map<Recipe>(entity);
        }
        
        public async Task<PagedResults<Recipe>> GetRecipesAsync(
            PagingOptions pagingOptions,
            SortOptions<Recipe, RecipeEntity> sortOptions,
            SearchOptions<Recipe, RecipeEntity> searchOptions)
        {
            IQueryable<RecipeEntity> query = this.context.Recipes;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();
            
            var items = await query.Skip(pagingOptions.Offset.Value).Take(pagingOptions.Limit.Value).ProjectTo<Recipe>(this.mappingConfiguration).ToArrayAsync();
            
            return new PagedResults<Recipe>
            {
                Items = items,
                TotalSize = size,
            };
        }

        public async Task<IEnumerable<Recipe>> FindRecipes(string ingredient = null)
        {
            if (string.IsNullOrWhiteSpace(ingredient))
            {
                return new Recipe[] { };
            }

            ingredient = ingredient.ToLowerInvariant();

            var entities = this.context.Recipes.Where(recipe => recipe.Ingredients.Any(v => v.Name.ToLowerInvariant().Contains(ingredient))).OrderBy(x => x.Title);

            var resources = entities.ProjectTo<Recipe>(this.mappingConfiguration);

            return await resources.ToListAsync();
        }
    }
}
