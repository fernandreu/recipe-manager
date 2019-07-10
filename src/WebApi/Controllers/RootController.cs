using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(this.GetRoot)),
                Recipes = Link.ToCollection(nameof(RecipesController.ListAllRecipes)),
                Users = Link.ToCollection(nameof(UsersController.ListAllUsers)),
            };

            return this.Ok(response);
        }
    }
}