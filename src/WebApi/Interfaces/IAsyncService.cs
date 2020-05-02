using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IAsyncService<TResource>
        where TResource : BaseResource
    {
        Task<TResource?> GetByIdAsync(Guid id);

        Task<PagedResults<TResource>> ListAsync(Specification<TResource> spec);

        Task<TResource> CreateAsync(TResource model);

        Task<TResource?> UpdateAsync(Guid id, TResource model);

        Task<int> DeleteAllAsync();
    }
}
