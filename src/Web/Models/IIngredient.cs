namespace RecipeManager.Web.Models
{
    public interface IIngredient
    {
        string Name { get; set; }

        double Quantity { get; set; }

        string Units { get; set; }
    }
}
