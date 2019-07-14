using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Models;

namespace RecipeManager.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private static readonly UserModel LoggedOutUser = new UserModel { IsAuthenticated = false };

        private readonly UserManager<IdentityUser> userManager;

        public AccountsController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser
            {
                UserName = model.Email, 
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return this.BadRequest(new RegisterResult
                {
                    Successful = false,
                    Errors = errors,
                });
            }

            return this.Ok(new RegisterResult
            {
                Successful = true,
            });
        }

        public IActionResult GetUser()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Ok(LoggedOutUser);
            }

            return this.Ok(new UserModel
            {
                Email = this.User.Identity.Name,
                IsAuthenticated = true,
            });
        }
    }
}
