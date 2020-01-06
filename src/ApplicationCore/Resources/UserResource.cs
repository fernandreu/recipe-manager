using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Resources
{
    public class UserResource : BaseResource
    {
        public string UserName { get; set; } = string.Empty;

        public IReadOnlyCollection<IngredientResource>? Ingredients { get; set; }
    }
}
