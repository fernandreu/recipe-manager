using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeApiDbContext context;

        private readonly IConfigurationProvider mappingConfiguration;

        public RecipeService(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            this.context = context;
            this.mappingConfiguration = mappingConfiguration;
        }

        public async Task<RecipeResource> GetByIdAsync(Guid id)
        {
            var entity = await this.context.Recipes.IncludeAll(true).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            var mapper = this.mappingConfiguration.CreateMapper();
            return mapper.Map<RecipeResource>(entity);
        }
        
        public async Task<PagedResults<RecipeResource>> ListAsync(ISpecification<Recipe> spec)
        {
            var query = this.context.Recipes.With(spec);
            var size = await query.CountAsync();
            var items = await query.ProjectTo<RecipeResource>(this.mappingConfiguration).ToArrayAsync();
            
            return new PagedResults<RecipeResource>
            {
                Items = items,
                TotalSize = size,
            };
        }
    }
}
