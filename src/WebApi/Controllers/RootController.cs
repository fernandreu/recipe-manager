using Microsoft.AspNetCore.Mvc;
using RecipeManager.WebApi.Resources;

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
                Recipes = Link.ToCollection(nameof(RecipesController.ListAll)),
            };

            return this.Ok(response);
        }
    }
}