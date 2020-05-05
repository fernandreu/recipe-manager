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

namespace RecipeManager.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly PagingOptions defaultPagingOptions;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly AppDbContext context;

        private readonly IConfigurationProvider mappingConfiguration;
        
        public IngredientsController(
            IOptions<PagingOptions> defaultPagingOptionsWrapper,
            UserManager<ApplicationUser> userManager,
            AppDbContext context,
            IConfigurationProvider mappingConfiguration)
        {
            defaultPagingOptions = defaultPagingOptionsWrapper?.Value ?? new PagingOptions();
            this.userManager = userManager;
            this.context = context;
            this.mappingConfiguration = mappingConfiguration;
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

            var spec = new Specification<IngredientResource>(options);
            
            var user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return Forbid();
            }
            
            var mapper = mappingConfiguration.CreateMapper();
            var entities = await context.UserIngredients
                .Where(x => x.UserId == user.Id)
                .IncludeAll(false)
                .ToListAsync()
                .ConfigureAwait(false);
            
            var items = entities.Select(x => mapper.Map<IngredientResource>(x)).ToArray();
            
            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(ListAllUserIngredients)), 
                items, 
                items.Length, 
                options.Paging);
        }
    }
}