// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonExceptionFilter.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the JsonExceptionFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Filters
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using RecipeManager.Models;

    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;

        public JsonExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (this.env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
