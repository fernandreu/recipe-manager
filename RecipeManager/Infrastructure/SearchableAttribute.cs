// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchableAttribute.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchableAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public ISearchExpressionProvider ExpressionProvider { get; set; } = new DefaultSearchExpressionProvider();
    }
}
