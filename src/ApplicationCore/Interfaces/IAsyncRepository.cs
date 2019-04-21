namespace RecipeManager.ApplicationCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Paging;
    using RecipeManager.ApplicationCore.Search;
    using RecipeManager.ApplicationCore.Sort;

    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<IEnumerable<T>> ListAsync(PagingOptions pagingOptions, SortOptions<T> sortOptions, SearchOptions<T> searchOptions);

        Task<int> CountAsync(SortOptions<T> sortOptions, SearchOptions<T> searchOptions);
    }
}
