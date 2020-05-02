using AutoMapper;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class RecipeService : ServiceBase<Recipe, RecipeResource>, IRecipeService
    {
        public RecipeService(AppDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }
    }
}
