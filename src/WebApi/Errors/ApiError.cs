using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Errors
{
    public class ApiError : BaseResource
    {
        public ApiError()
        {
        }

        public ApiError(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            Message = message;
        }
        
        public ApiError(ModelStateDictionary modelState)
        {
            StatusCode = 400;
            StatusDescription = "Invalid parameters.";
            Message = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault()?.ErrorMessage;
        }

        public int StatusCode { get; private set; }

        public string? StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Message { get; private set; }
    }
}
