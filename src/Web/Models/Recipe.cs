// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Recipe type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using System.Collections.Generic;

    using RecipeManager.Web.Infrastructure;

    public class Recipe : Resource, IRecipe<Ingredient>
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Title { get; set; }
        
        [SearchableIngredients]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Sortable]
        [Searchable]
        public string Details { get; set; }
    }
}
