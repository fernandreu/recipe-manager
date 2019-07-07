using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.WebApi.Helpers;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeResourceService recipeService;

        private readonly PagingOptions defaultPagingOptions;

        public RecipesController(
            IRecipeResourceService recipeService,
            IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            this.recipeService = recipeService;
            this.defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }
        
        [HttpGet(Name = nameof(ListAll))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<RecipeResource>>> ListAll(
            [FromQuery] PagingOptions pagingOptions, 
            [FromQuery] SortOptions<Recipe> sortOptions,
            [FromQuery] SearchOptions<Recipe> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? this.defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? this.defaultPagingOptions.Limit;

            var spec = new RecipeSpecification(pagingOptions, searchOptions, sortOptions);
            var recipes = await this.recipeService.ListAsync(spec);

            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(this.ListAll)), 
                recipes.Items.ToArray(), 
                recipes.TotalSize, 
                pagingOptions);
        }

        [HttpGet("{recipeId}", Name = nameof(GetRecipeById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RecipeResource>> GetRecipeById(Guid recipeId)
        {
            var recipe = await this.recipeService.GetByIdAsync(recipeId);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return recipe;
        }
    }
}