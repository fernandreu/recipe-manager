// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipesController.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the RecipesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using RecipeManager.Models;
    using RecipeManager.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet(Name = nameof(GetAllRecipes))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Recipe>>> GetAllRecipes(string ingredient = null)
        {
            if (!this.Request.Query.ContainsKey(nameof(ingredient)))
            {
                var allRecipes = await this.recipeService.GetRecipesAsync();

                return new Collection<Recipe>
                {
                    Self = Link.To(nameof(this.GetAllRecipes)),
                    Value = allRecipes.ToArray(),
                };
            }
            
            var filteredRecipes = await this.recipeService.FindRecipes(ingredient);
            return new Collection<Recipe>
            {
                // Link parameters are set to an empty string if needed so that clicking on that link truly produces the same query
                Self = Link.To(nameof(this.GetAllRecipes), new { ingredient = ingredient ?? string.Empty }),
                Value = filteredRecipes.ToArray(),
            };
        }

        [HttpGet("{recipeId}", Name = nameof(GetRecipeById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Recipe>> GetRecipeById(Guid recipeId)
        {
            var recipe = await this.recipeService.GetRecipeAsync(recipeId);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return recipe;
        }
    }
}