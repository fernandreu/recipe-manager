// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagingOptions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the PagingOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PagingOptions
    {
        [Range(1, 99999, ErrorMessage = "Offset must be greater than 0.")]
        public int? Offset { get; set; }
        
        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? Limit { get; set; }

        public PagingOptions Replace(PagingOptions newer)
        {
            return new PagingOptions
            {
                Offset = newer.Offset ?? this.Offset,
                Limit = newer.Limit ?? this.Limit,
            };
        }
    }
}
