using System;
using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface IRecipe<TIngredient> where TIngredient : IIngredient
    {
        Guid Id { get; set; }

        string Title { get; set; }
        
        string? Details { get; set; }
        
        ICollection<TIngredient>? Ingredients { get; }
    }
}
