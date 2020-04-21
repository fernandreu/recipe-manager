using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using RecipeManager.WebApi.Errors;

namespace RecipeManager.WebApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;

        public JsonExceptionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "ASP.Net Core would not pass a null argument")]
        public void OnException(ExceptionContext context)
        {
            ApiError error;
            
            if (env.IsDevelopment())
            {
                context.ExceptionHandled = false;
                return;
                //error = new ApiError(
                //    500, 
                //    context.Exception.Message,
                //    context.Exception.StackTrace);
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
