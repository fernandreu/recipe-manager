namespace RecipeManager.Models
{
    using System.Collections.Generic;

    public interface IRecipe<TIngredient> where TIngredient : IIngredient
    {
        string Title { get; set; }

        ICollection<TIngredient> Ingredients { get; }
    }
}
