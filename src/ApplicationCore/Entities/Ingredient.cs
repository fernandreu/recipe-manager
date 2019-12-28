using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Entities
{
    public class Ingredient : BaseEntity, IIngredient
    {
        public string Name { get; set; } = string.Empty;

        public double Quantity { get; set; }

        public string? Units { get; set; }
    }
}
