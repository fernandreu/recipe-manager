namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface IIngredient
    {
        string Name { get; set; }

        double Quantity { get; set; }

        string? Units { get; set; }
    }
}
