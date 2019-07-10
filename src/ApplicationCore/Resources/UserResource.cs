using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Resources
{
    public class UserResource : BaseResource
    {
        public string UserName { get; set; }

        public ICollection<IngredientResource> Ingredients { get; set; }
    }
}
