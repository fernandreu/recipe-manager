using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class RecipeResourceService : IRecipeResourceService
    {
        private readonly IRecipeRepository recipeRepository;

        private readonly IMapper mapper;

        public RecipeResourceService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            this.recipeRepository = recipeRepository;
            this.mapper = mapper;
        }

        public async Task<RecipeResource> GetByIdAsync(Guid id)
        {
            var entity = await this.recipeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            
            return this.mapper.Map<RecipeResource>(entity);
        }
        
        public async Task<PagedResults<RecipeResource>> ListAsync(ISpecification<Recipe> spec)
        {
            var size = await this.recipeRepository.CountAsync(spec);
            var items = await this.recipeRepository.ListAsync(spec);
            
            return new PagedResults<RecipeResource>
            {
                Items = items.Select(x => this.mapper.Map<RecipeResource>(x)),
                TotalSize = size,
            };
        }
    }
}
