using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeManager.WebApi.Errors;

namespace RecipeManager.WebApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;

        public JsonExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            ApiError error;
            
            if (this.env.IsDevelopment())
            {
                error = new ApiError(
                    500, 
                    context.Exception.Message,
                    context.Exception.StackTrace);
            }
            else
            {
                error = new ApiError(
                    500,
                    "A server error occurred",
                    context.Exception.Message);
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = error.StatusCode
            };
        }
    }
}
