using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;
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
        
        public async Task<PagedResults<RecipeResource>> ListAsync(
            PagingOptions pagingOptions,
            SortOptions<Recipe> sortOptions,
            SearchOptions<Recipe> searchOptions)
        {
            var size = await this.recipeRepository.CountAsync(sortOptions, searchOptions);
            var items = await this.recipeRepository.ListAsync(pagingOptions, sortOptions, searchOptions);
            
            return new PagedResults<RecipeResource>
            {
                Items = items.Select(x => this.mapper.Map<RecipeResource>(x)),
                TotalSize = size,
            };
        }
    }
}
