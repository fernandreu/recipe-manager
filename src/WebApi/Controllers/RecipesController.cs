using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.WebApi.Helpers;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;

        private readonly PagingOptions defaultPagingOptions;

        public RecipesController(
            IRecipeService recipeService,
            IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            this.recipeService = recipeService;
            defaultPagingOptions = defaultPagingOptionsWrapper?.Value ?? new PagingOptions();
        }
        
        [HttpGet(Name = nameof(ListAllRecipes))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<RecipeResource>>> ListAllRecipes(
            [FromQuery] SpecificationOptions<Recipe> options)
        {
            options ??= new SpecificationOptions<Recipe>();
            options.Paging ??= defaultPagingOptions;
            options.Paging.Offset ??= defaultPagingOptions.Offset;
            options.Paging.Limit ??= defaultPagingOptions.Limit;

            var spec = new RecipeSpecification(options);
            var recipes = await recipeService.ListAsync(spec).ConfigureAwait(false);

            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(ListAllRecipes)), 
                recipes.Items.ToArray(), 
                recipes.TotalSize, 
                options.Paging);
        }

        [HttpGet("{recipeId}", Name = nameof(GetRecipeById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> GetRecipeById(Guid recipeId)
        {
            var result = await recipeService.GetByIdAsync(recipeId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost(Name = nameof(CreateRecipe))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> CreateRecipe(RecipeResource model)
        {
            var result = await recipeService.CreateAsync(model).ConfigureAwait(false);
            return result;
        }

        [HttpPut("{recipeId}", Name = nameof(UpdateRecipe))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> UpdateRecipe(Guid recipeId, RecipeResource model)
        {
            var result = await recipeService.UpdateAsync(recipeId, model).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}