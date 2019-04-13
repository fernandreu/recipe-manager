// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ingredient.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Ingredient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using RecipeManager.Extensions;

    public class Ingredient : Resource, IIngredient
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Units { get; set; }

        public string FullName => this.FullName();
    }
}
