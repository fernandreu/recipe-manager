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

        Task<IEnumerable<Recipe>> GetRecipesAsync();

        Task<IEnumerable<Recipe>> FindRecipes(string ingredient = null);
    }
}
