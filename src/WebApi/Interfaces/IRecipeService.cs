using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeResource> GetByIdAsync(Guid id);

        Task<PagedResults<RecipeResource>> ListAsync(ISpecification<Recipe> spec);
    }
}
