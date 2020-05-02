using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Specifications;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IAsyncService<TEntity, TResource>
        where TEntity : ISingleEntity
        where TResource : BaseResource
    {
        Task<TResource?> GetByIdAsync(Guid id);

        Task<PagedResults<TResource>> ListAsync(Specification<TEntity> spec);

        Task<TResource> CreateAsync(TResource model);

        Task<TResource?> UpdateAsync(Guid id, TResource model);

        Task<int> DeleteAllAsync();
    }
}
