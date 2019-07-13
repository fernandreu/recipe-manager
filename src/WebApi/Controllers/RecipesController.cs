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
            this.defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }
        
        [HttpGet(Name = nameof(ListAllRecipes))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<RecipeResource>>> ListAllRecipes(
            [FromQuery] SpecificationOptions<Recipe> options)
        {
            if (options.Paging == null)
            {
                options.Paging = this.defaultPagingOptions;
            }

            options.Paging.Offset = options.Paging.Offset ?? this.defaultPagingOptions.Offset;
            options.Paging.Limit = options.Paging.Limit ?? this.defaultPagingOptions.Limit;

            var spec = new RecipeSpecification(options);
            var recipes = await this.recipeService.ListAsync(spec);

            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(this.ListAllRecipes)), 
                recipes.Items.ToArray(), 
                recipes.TotalSize, 
                options.Paging);
        }

        [HttpGet("{recipeId}", Name = nameof(GetRecipeById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> GetRecipeById(Guid recipeId)
        {
            var result = await this.recipeService.GetByIdAsync(recipeId);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }

        [HttpPost(Name = nameof(CreateRecipe))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> CreateRecipe(RecipeResource model)
        {
            var result = await this.recipeService.CreateAsync(model);
            return result;
        }

        [HttpPut("{recipeId}", Name = nameof(UpdateRecipe))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> UpdateRecipe(Guid recipeId, RecipeResource model)
        {
            var result = await this.recipeService.UpdateAsync(recipeId, model);
            if (result == null)
            {
                return this.NotFound();
            }

            return result;
        }
    }
}