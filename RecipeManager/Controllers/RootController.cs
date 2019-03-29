// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootController.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the RootController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RecipeManager.Models;

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
                Recipes = Link.ToCollection(nameof(RecipesController.GetAllRecipes)),
            };

            return this.Ok(response);
        }
    }
}