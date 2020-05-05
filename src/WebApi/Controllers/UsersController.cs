using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.WebApi.Helpers;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly PagingOptions defaultPagingOptions;

        public UsersController(
            IUserService userService,
            IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            this.userService = userService;
            defaultPagingOptions = defaultPagingOptionsWrapper?.Value ?? new PagingOptions();
        }
        
        [Authorize]
        [HttpGet(Name = nameof(ListAllUsers))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<UserResource>>> ListAllUsers(
            [FromQuery] SpecificationOptions<UserResource> options)
        {
            options ??= new SpecificationOptions<UserResource>();
            options.Paging ??= defaultPagingOptions;
            options.Paging.Offset ??= defaultPagingOptions.Offset;
            options.Paging.Limit ??= defaultPagingOptions.Limit;

            var spec = new Specification<UserResource>(options);
            var users = await userService.ListAsync(spec).ConfigureAwait(false);

            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(ListAllUsers)), 
                users.Items.ToArray(), 
                users.TotalSize, 
                options.Paging);
        }

        [HttpGet("{userId}", Name = nameof(GetUserById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserResource>> GetUserById(Guid userId)
        {
            var user = await userService.GetByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return NoContent();
            }

            return user;
        }
    }
}