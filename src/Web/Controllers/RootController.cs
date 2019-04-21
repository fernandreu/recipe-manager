namespace RecipeManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RecipeManager.Web.Resources;

    [Route("api/")]
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