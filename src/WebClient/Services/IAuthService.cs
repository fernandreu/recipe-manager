using System.Threading.Tasks;
using RecipeManager.ApplicationCore.Models;

namespace WebClient.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        
        Task Logout();
        
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
