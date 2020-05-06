using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IIngredientService : IAsyncService<IngredientResource>
    {
        Task<PagedResults<IngredientResource>> ListAsync(Specification<IngredientResource> spec, Guid userId);
    }
}
