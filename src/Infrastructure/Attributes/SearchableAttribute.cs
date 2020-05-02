using System;
using RecipeManager.Infrastructure.Interfaces;
using RecipeManager.Infrastructure.Search;

namespace RecipeManager.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; } = new SearchExpressionProvider();
    }
}
