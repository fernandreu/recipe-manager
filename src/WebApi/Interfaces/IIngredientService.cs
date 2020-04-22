using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IIngredientService : IAsyncService<RecipeIngredient, IngredientResource>
    {
    }
}
