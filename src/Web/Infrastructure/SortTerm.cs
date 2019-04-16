// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortTerm.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SortTerm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    public class SortTerm
    {
        public string Name { get; set; }

        public string EntityName { get; set; }

        public bool Descending { get; set; }

        public bool Default { get; set; }
    }
}
