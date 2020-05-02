// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeIngredient.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace RecipeManager.Infrastructure.Entities
{
    public class RecipeIngredient : IngredientBase
    {
        public Guid? RecipeId { get; set; }
        
        public virtual Recipe? Recipe { get; set; }
    }
}