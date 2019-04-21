namespace RecipeManager.Web.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Paging;
    using RecipeManager.ApplicationCore.Search;
    using RecipeManager.ApplicationCore.Sort;
    using RecipeManager.Web.Resources;

    public interface IRecipeResourceService
    {
        Task<RecipeResource> GetByIdAsync(Guid id);

        Task<PagedResults<RecipeResource>> ListAsync(
            PagingOptions pagingOptions, 
            SortOptions<Recipe> sortOptions,
            SearchOptions<Recipe> searchOptions);
    }
}
