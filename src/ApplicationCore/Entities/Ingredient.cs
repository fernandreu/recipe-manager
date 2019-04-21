namespace RecipeManager.ApplicationCore.Entities
{
    using RecipeManager.ApplicationCore.Interfaces;

    public class Ingredient : BaseEntity, IIngredient
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Units { get; set; }
    }
}
