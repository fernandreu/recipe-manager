using System.Collections.Generic;

namespace WebClient.Resources
{
    public class RecipeResource : BaseResource
    {
        public string Title { get; set; }
        
        public ICollection<IngredientResource> Ingredients { get; set; }

        public string Details { get; set; }
    }
}
