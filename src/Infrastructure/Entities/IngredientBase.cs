using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.Infrastructure.Entities
{
    public class IngredientBase : SingleEntity, IIngredient
    {
        public string Name { get; set; } = string.Empty;

        public double Quantity { get; set; }

        public string? Units { get; set; }
    }
}
