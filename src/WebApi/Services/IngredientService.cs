using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class IngredientService : ServiceBase<RecipeIngredient, IngredientResource>, IIngredientService
    {
        public IngredientService(AppDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }
    }
}
