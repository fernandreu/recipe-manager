using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IUserService : IAsyncService<User, UserResource>
    {
    }
}
