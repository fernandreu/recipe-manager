using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RecipeManager.ApplicationCore.Attributes;

namespace RecipeManager.ApplicationCore.Entities
{
    public class User : SingleEntity
    {
        public string UserName { get; set; } = string.Empty;

        [IncludeInSingleQueries]
        [SearchableIngredients]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Compromise solution for EFCore")]
        public ICollection<UserIngredient>? Ingredients { get; set; }
    }
}
