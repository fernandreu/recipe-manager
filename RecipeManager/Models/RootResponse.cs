// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootResponse.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the RootResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    public class RootResponse : Resource
    {
        public Link Recipes { get; set; }
    }
}
