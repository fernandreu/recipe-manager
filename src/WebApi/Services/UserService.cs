using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class UserService : ServiceBase<User, UserResource>, IUserService
    {
        public UserService(RecipeApiDbContext context, IConfigurationProvider mappingConfiguration) : 
            base(context, mappingConfiguration)
        {
        }
    }
}
