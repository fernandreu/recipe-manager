using RecipeManager.ApplicationCore.Search;

namespace RecipeManager.ApplicationCore.Attributes
{
    // TODO: Both this and the corresponding expression provider should be made as generic as possible
    // Ideally, any item property of any IEnumerable<T> collection could be the target of a search.
    // Why not inheriting those search properties from the T class? For example, an individual ingredient search could
    // look like "/ingredients?search=name eq salt", while from recipes, it could be mapped to "/recipes?search=ingredientName eq salt"
    public class SearchableIngredientsAttribute : SearchableAttribute
    {
        public SearchableIngredientsAttribute()
        {
            ExpressionProvider = new IngredientExpressionProvider();
        }
    }
}
