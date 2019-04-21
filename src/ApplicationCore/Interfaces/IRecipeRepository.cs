namespace RecipeManager.ApplicationCore.Interfaces
{
    using RecipeManager.ApplicationCore.Entities;

    public interface IRecipeRepository : IAsyncRepository<Recipe>
    {
    }
}
