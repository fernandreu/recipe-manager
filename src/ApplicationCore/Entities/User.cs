using System.Collections.Generic;
using RecipeManager.ApplicationCore.Attributes;

namespace RecipeManager.ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        [IncludeInSingleQueries]
        [SearchableIngredients]
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
