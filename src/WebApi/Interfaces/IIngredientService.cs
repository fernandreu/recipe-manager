using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IIngredientService : IAsyncService<RecipeIngredient, IngredientResource>
    {
    }
}
