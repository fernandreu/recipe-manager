﻿using System;
using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IAsyncService<TEntity, TResource>
        where TEntity : BaseEntity
        where TResource : BaseResource
    {
        Task<TResource> GetByIdAsync(Guid id);

        Task<PagedResults<TResource>> ListAsync(ISpecification<TEntity> spec);
    }
}