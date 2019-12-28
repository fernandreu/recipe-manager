using System.Collections.Generic;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Entities
{
    public class Recipe : BaseEntity, IRecipe<Ingredient>
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Title { get; set; } = string.Empty;
        
        [IncludeInAllQueries]
        [SearchableIngredients]
        public ICollection<Ingredient>? Ingredients { get; set; }
        
        [Sortable]
        [Searchable]
        public string? Details { get; set; }
    }
}
