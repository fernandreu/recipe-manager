// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserIngredient.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace RecipeManager.Infrastructure.Entities
{
    public class UserIngredient : IngredientBase
    {
        public Guid? UserId { get; set; }
        
        public virtual ApplicationUser? User { get; set; }
    }
}