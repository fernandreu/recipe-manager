namespace RecipeManager.ApplicationCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRecipe<TIngredient> where TIngredient : IIngredient
    {
        Guid Id { get; set; }

        string Title { get; set; }

        ICollection<TIngredient> Ingredients { get; }
    }
}
