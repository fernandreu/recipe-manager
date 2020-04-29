using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Resources
{
    public class RecipeResource : BaseResource
    {
        public string Title { get; set; } = string.Empty;
        
        public ICollection<IngredientResource>? Ingredients { get; set; }

        public string? Details { get; set; }
    }
}
