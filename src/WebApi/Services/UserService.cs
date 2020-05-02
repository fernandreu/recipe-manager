using AutoMapper;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class UserService : ServiceBase<ApplicationUser, UserResource>, IUserService
    {
        public UserService(AppDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }
    }
}
