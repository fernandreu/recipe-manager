// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRecipeService.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the IRecipeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RecipeManager.Models;

    public interface IRecipeService
    {
        Task<Recipe> GetRecipeAsync(Guid id);

        Task<PagedResults<Recipe>> GetRecipesAsync(
            PagingOptions pagingOptions, 
            SortOptions<Recipe, RecipeEntity> sortOptions,
            SearchOptions<Recipe, RecipeEntity> searchOptions);

        Task<IEnumerable<Recipe>> FindRecipes(string ingredient = null);
    }
}
