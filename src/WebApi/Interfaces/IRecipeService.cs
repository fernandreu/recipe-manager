using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IRecipeService : IAsyncService<Recipe, RecipeResource>
    {
    }
}
