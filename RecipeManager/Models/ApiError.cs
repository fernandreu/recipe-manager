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
    public class ApiError
    {
        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
