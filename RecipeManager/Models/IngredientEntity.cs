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

    public class IngredientEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
