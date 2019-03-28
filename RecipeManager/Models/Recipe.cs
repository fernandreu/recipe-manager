// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Recipe type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using System.Collections.Generic;

    public class Recipe : Resource
    {
        public string Title { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public string Details { get; set; }
    }
}
