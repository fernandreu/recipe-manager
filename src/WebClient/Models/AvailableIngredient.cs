// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AvailableIngredient.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 09/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using RecipeManager.ApplicationCore.Resources;

namespace WebClient.Models
{
    public class AvailableIngredient : IngredientResource
    {
        public bool IsAvailable { get; set; }
    }
}