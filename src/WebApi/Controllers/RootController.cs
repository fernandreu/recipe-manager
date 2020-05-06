using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Recipes = Link.ToCollection(nameof(RecipesController.ListAllRecipes)),
                Users = Link.ToCollection(nameof(UsersController.ListAllUsers)),
            };

            return Ok(response);
        }
    }
}