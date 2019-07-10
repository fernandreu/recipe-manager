using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IRecipeService : IAsyncService<Recipe, RecipeResource>
    {
    }
}
