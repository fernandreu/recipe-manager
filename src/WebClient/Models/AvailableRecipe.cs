// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailableRecipe.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 09/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;

namespace WebClient.Models
{
    public class AvailableRecipe : BaseResource, IRecipe<AvailableIngredient>
    {
        public string Title { get; set; }
        
        public ICollection<AvailableIngredient> Ingredients { get; set; }

        public string Details { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}