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
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly PagingOptions defaultPagingOptions;

        public UsersController(
            IUserService userService,
            IOptions<PagingOptions> defaultPagingOptionsWrapper)
        {
            this.userService = userService;
            this.defaultPagingOptions = defaultPagingOptionsWrapper.Value;
        }
        
        [HttpGet(Name = nameof(ListAllUsers))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedCollection<UserResource>>> ListAllUsers(
            [FromQuery] SpecificationOptions<User> options)
        {
            if (options.Paging == null)
            {
                options.Paging = this.defaultPagingOptions;
            }

            options.Paging.Offset = options.Paging.Offset ?? this.defaultPagingOptions.Offset;
            options.Paging.Limit = options.Paging.Limit ?? this.defaultPagingOptions.Limit;

            var spec = new UserSpecification(options);
            var users = await this.userService.ListAsync(spec);

            return PagedCollectionHelper.Create(
                Link.ToCollection(nameof(this.ListAllUsers)), 
                users.Items.ToArray(), 
                users.TotalSize, 
                options.Paging);
        }

        [HttpGet("{userId}", Name = nameof(GetUserById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserResource>> GetUserById(Guid userId)
        {
            var user = await this.userService.GetByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            return user;
        }
    }
}