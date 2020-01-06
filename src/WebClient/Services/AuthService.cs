using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using RecipeManager.ApplicationCore.Models;
using WebClient.Login;
using WebClient.Models;

namespace WebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;

        private readonly AuthenticationStateProvider authenticationStateProvider;

        private readonly ILocalStorageService localStorage;

        private readonly ServerConfig serverConfig;

        public AuthService(
            HttpClient httpClient, 
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage,
            ServerConfig serverConfig)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorage = localStorage;
            this.serverConfig = serverConfig;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var json = JsonConvert.SerializeObject(registerModel);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync(this.serverConfig.UrlTo("accounts"), content);
            var result = JsonConvert.DeserializeObject<RegisterResult>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var result = await this.httpClient.PostJsonAsync<LoginResult>(this.serverConfig.UrlTo("login"), loginModel);

            if (result.Successful)
            {
                await this.localStorage.SetItemAsync("authToken", result.Token);
                ((ApiAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return result;
            }

            return result;
        }

        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsLoggedOut();
            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
