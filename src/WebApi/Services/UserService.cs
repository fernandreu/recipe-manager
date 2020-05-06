using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.WebApi.Interfaces;

namespace RecipeManager.WebApi.Services
{
    public class UserService : ServiceBase<ApplicationUser, UserResource>, IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        
        public UserService(AppDbContext context, IConfigurationProvider mappingConfiguration, UserManager<ApplicationUser> userManager) : 
            base(context, mappingConfiguration)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> SetPasswordAsync(string username, string password)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.UserName == username).ConfigureAwait(false);
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            var result = await userManager.ResetPasswordAsync(user, token, password).ConfigureAwait(false);
            return result;
        }
    }
}
