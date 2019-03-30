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
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using RecipeManager.Models;
    using RecipeManager.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;

        private readonly PagingOptions defaultPagingOptions;

        public RecipesController(IRecipeService recipeService, IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            this.recipeService = recipeService;
            this.defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }
        
        [HttpGet(Name = nameof(GetAllRecipes))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<Recipe>>> GetAllRecipes(
            [FromQuery] PagingOptions pagingOptions, 
            [FromQuery] SortOptions<Recipe, RecipeEntity> sortOptions,
            [FromQuery] SearchOptions<Recipe, RecipeEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? this.defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? this.defaultPagingOptions.Limit;

            var recipes = await this.recipeService.GetRecipesAsync(pagingOptions, sortOptions, searchOptions);

            return PagedCollection<Recipe>.Create(
                Link.ToCollection(nameof(this.GetAllRecipes)), 
                recipes.Items.ToArray(), 
                recipes.TotalSize, 
                pagingOptions);
        }

        [HttpGet("search", Name = nameof(FindRecipes))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Recipe>>> FindRecipes(string ingredient = null)
        {
            var filteredRecipes = await this.recipeService.FindRecipes(ingredient);
            return new Collection<Recipe>
            {
                // Link parameters are set to an empty string if needed so that clicking on that link truly produces the same query
                Self = Link.ToCollection(nameof(this.FindRecipes), new { ingredient = ingredient ?? string.Empty }),
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