namespace RecipeManager.Web.Resources
{
    using System.Collections.Generic;
    
    public class RecipeResource : BaseResource
    {
        public string Title { get; set; }
        
        public ICollection<IngredientResource> Ingredients { get; set; }

        public string Details { get; set; }
    }
}
