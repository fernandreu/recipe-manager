using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Search
{
    public class SearchTerm
    {
        public string Name { get; set; } = string.Empty;

        public string Operator { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; } = new SearchExpressionProvider();
    }
}
