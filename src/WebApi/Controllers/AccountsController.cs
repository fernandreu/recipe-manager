using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Models;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private static readonly UserModel LoggedOutUser = new UserModel { IsAuthenticated = false };

        private readonly UserManager<ApplicationUser> userManager;

        public AccountsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return Ok(new RegisterResult
                {
                    Successful = false,
                    Errors = new[] {"No registration data passed"},
                });
            }

            var newUser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(newUser, model.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new RegisterResult
                {
                    Successful = false,
                    Errors = errors,
                });
            }

            return Ok(new RegisterResult
            {
                Successful = true,
            });
        }

        [HttpGet("user")]
        public ActionResult<UserModel> ReadUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(LoggedOutUser);
            }

            var name = User.Identity.Name;
            if (name == null)
            {
                return NotFound();
            }

            return Ok(new UserModel
            {
                UserName = name,
                IsAuthenticated = true,
            });
        }
    }
}
