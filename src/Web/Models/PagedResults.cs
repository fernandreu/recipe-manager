// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedResults.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the PagedResults type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using System.Collections.Generic;

    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
