using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Resources
{
    public class IngredientResource : BaseResource, IIngredient
    {
        public string Name { get; set; } = string.Empty;

        public double Quantity { get; set; }

        public string? Units { get; set; }
    }
}
