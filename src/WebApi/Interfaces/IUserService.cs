using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IUserService : IAsyncService<ApplicationUser, UserResource>
    {
    }
}
