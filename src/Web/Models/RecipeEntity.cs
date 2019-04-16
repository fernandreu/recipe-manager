// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeEntity.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the RecipeEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class RecipeEntity : IRecipe<IngredientEntity>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public ICollection<IngredientEntity> Ingredients { get; set; }

        public string Details { get; set; }
    }
}
