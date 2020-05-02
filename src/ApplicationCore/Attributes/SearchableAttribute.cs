using System;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
