using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.WebApi.Helpers;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly PagingOptions defaultPagingOptions;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IIngredientService ingredientService;

        public IngredientsController(
            IOptions<PagingOptions> defaultPagingOptionsWrapper,
            UserManager<ApplicationUser> userManager,
            IIngredientService ingredientService)
        {
            defaultPagingOptions = defaultPagingOptionsWrapper?.Value ?? new PagingOptions();
            this.userManager = userManager;
            this.ingredientService = ingredientService;
        }
        
        [HttpGet(Name = nameof(ListAllUserIngredients))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<IngredientResource>>> ListAllUserIngredients(
            [FromQuery] SpecificationOptions<IngredientResource> options)
        {
            options ??= new SpecificationOptions<IngredientResource>();
            options.Paging ??= defaultPagingOptions;
            options.Paging.Offset ??= defaultPagingOptions.Offset;
            options.Paging.Limit ??= defaultPagingOptions.Limit;

            var user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return Forbid();
            }
            
            var spec = new Specification<IngredientResource>(options);
            var items = await ingredientService.ListAsync(spec, user.Id).ConfigureAwait(false);
            
            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(ListAllUserIngredients)), 
                items.Items.ToArray(), 
                items.TotalSize, 
                options.Paging);
        }
    }
}