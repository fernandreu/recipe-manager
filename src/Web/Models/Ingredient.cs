// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ingredient.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Ingredient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using RecipeManager.Web.Extensions;

    public class Ingredient : Resource, IIngredient
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Units { get; set; }
        
        /// <summary>
        /// Gets a combination of quantity, units and name of this ingredient (e.g. "1kg rice")
        /// </summary>
        /// The explicit definition of FullName as a property (and not just the extension method) means it will be
        /// automatically shown when returned in a controller's method. As it is get-only, Entity Framework will not
        /// store it in the database even if it was defined in the IngredientEntity
        public string FullName => this.FullName();
    }
}
