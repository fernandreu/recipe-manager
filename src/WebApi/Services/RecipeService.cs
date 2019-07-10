using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class RecipeService : ServiceBase<Recipe, RecipeResource>, IRecipeService
    {
        public RecipeService(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }
    }
}
