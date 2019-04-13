// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IngredientEntity.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the IngredientEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using System;

    public class IngredientEntity : IIngredient
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Units { get; set; }
    }
}
