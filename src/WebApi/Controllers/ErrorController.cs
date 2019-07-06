using System.Net;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.WebApi.Errors;

namespace RecipeManager.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("{statusCode}", Name = nameof(HandleStatusCode))]
        [HttpPut("{statusCode}", Name = nameof(HandleStatusCode))]
        public ActionResult<ApiError> HandleStatusCode(int statusCode)
        {
            var parsedCode = (HttpStatusCode)statusCode;
            var error = new ApiError(statusCode, parsedCode.ToString())
            {
                Self = Link.To(nameof(this.HandleStatusCode), new { statusCode }),
            };
            return error;
        }
    }
}
