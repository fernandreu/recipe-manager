// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchTerm.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchTerm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    public class SearchTerm
    {
        public string Name { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
