using System.Collections.Generic;
using System.Linq;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;

namespace RecipeManager.ApplicationCore.Extensions
{
    /// <summary>
    /// Defines all instance methods that are common to both ingredient resources and ingredient entities
    /// </summary>
    public static class IngredientExtensions
    {
        public static bool IsMatch<T>(this IEnumerable<T> ingredients, string query)
            where T : IIngredient
        {
            if (ingredients == null)
            {
                return false;
            }
            
            var searchTerm = IngredientSearchTerm.Parse(query);
            if (searchTerm == null)
            {
                // TODO: Perhaps this should raise an exception
                return false;
            }

            return ingredients.Any(x => searchTerm.IsMatch(x));
        }
    }
}
