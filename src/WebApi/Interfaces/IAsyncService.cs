using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IAsyncService<TEntity, TResource>
        where TEntity : SingleEntity
        where TResource : BaseResource
    {
        Task<TResource?> GetByIdAsync(Guid id);

        Task<PagedResults<TResource>> ListAsync(ISpecification<TEntity> spec);

        Task<TResource> CreateAsync(TResource model);

        Task<TResource?> UpdateAsync(Guid id, TResource model);

        Task<int> DeleteAllAsync();
    }
}
