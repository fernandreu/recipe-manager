// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiError.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the ApiError type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace RecipeManager.Models
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ApiError
    {
        public ApiError()
        {
        }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }
        
        public ApiError(ModelStateDictionary modelState)
        {
            this.StatusCode = 400;
            this.StatusDescription = "Invalid parameters.";
            this.Message = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
        }

        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }
    }
}
