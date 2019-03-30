// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchableDecimalAttribute.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchableDecimalAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableDecimalAttribute : SearchableAttribute
    {
        public SearchableDecimalAttribute()
        {
            this.ExpressionProvider = new DecimalToIntSearchExpressionProvider();
        }
    }
}
