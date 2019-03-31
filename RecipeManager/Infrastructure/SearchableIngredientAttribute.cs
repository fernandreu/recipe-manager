// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchableIngredientAttribute.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchableIngredientAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    public class SearchableIngredientAttribute : SearchableAttribute
    {
        public SearchableIngredientAttribute()
        {
            this.ExpressionProvider = new IngredientExpressionProvider();
        }
    }
}
