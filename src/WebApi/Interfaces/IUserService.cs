using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi.Interfaces
{
    public interface IUserService : IAsyncService<UserResource>
    {
        /// <summary>
        /// Automates the process of generating a password reset token and assigning a password for a given user. This
        /// is mostly for admin operations; end users should do the two steps separately. 
        /// </summary>
        /// <param name="email">The email for which the password will be reset</param>
        /// <param name="password">The new password</param>
        /// <returns></returns>
        Task<IdentityResult> SetPasswordAsync(string email, string password);
    }
}
