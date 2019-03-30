// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiError.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the ApiError type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ApiError
    {
        public ApiError()
        {
        }

        public ApiError(ModelStateDictionary modelState)
        {
            this.Message = "Invalid parameters.";
            this.Detail = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
        }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
