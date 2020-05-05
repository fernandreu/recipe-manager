using System.Collections.Generic;
using RecipeManager.ApplicationCore.Attributes;

namespace RecipeManager.ApplicationCore.Resources
{
    public class UserResource : BaseResource
    {
        public string Email { get; set; } = string.Empty;

        [IncludeInSingleQueries]
        [SearchableIngredients]
        public IReadOnlyCollection<IngredientResource>? Ingredients { get; set; }
    }
}
