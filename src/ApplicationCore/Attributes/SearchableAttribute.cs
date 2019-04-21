namespace RecipeManager.ApplicationCore.Attributes
{
    using System;

    using RecipeManager.ApplicationCore.Interfaces;
    using RecipeManager.ApplicationCore.Search;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; } = new SearchExpressionProvider();
    }
}
