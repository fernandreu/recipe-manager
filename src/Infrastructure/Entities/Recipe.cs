using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.Infrastructure.Entities
{
    public class Recipe : SingleEntity, IRecipe<RecipeIngredient>
    {
        public string Title { get; set; } = string.Empty;
        
        [IncludeInAllQueries]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Compromise solution for EFCore")]
        public ICollection<RecipeIngredient>? Ingredients { get; set; }
        
        public string? Details { get; set; }
    }
}
