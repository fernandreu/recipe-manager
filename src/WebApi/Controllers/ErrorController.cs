using System.Net;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.WebApi.Errors;

namespace RecipeManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet("{statusCode}", Name = nameof(HandleStatusCode))]
        [HttpPut("{statusCode}", Name = nameof(HandleStatusCode))]
        [HttpDelete("{statusCode}", Name = nameof(HandleStatusCode))]
        public ActionResult<ApiError> HandleStatusCode(int statusCode)
        {
            var parsedCode = (HttpStatusCode)statusCode;
            var error = new ApiError(statusCode, parsedCode.ToString())
            {
                Self = Link.To(nameof(HandleStatusCode), new { statusCode }),
            };
            return error;
        }
    }
}
