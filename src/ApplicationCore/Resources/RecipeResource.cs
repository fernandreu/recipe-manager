using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RecipeManager.ApplicationCore.Attributes;

namespace RecipeManager.ApplicationCore.Resources
{
    public class RecipeResource : BaseResource
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Title { get; set; } = string.Empty;
        
        [SearchableIngredients]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Compromise solution for EFCore")]
        public ICollection<IngredientResource>? Ingredients { get; set; }

        [Sortable]
        [Searchable]
        public string? Details { get; set; }
    }
}
