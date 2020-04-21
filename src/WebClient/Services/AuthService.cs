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
            var response = await httpClient.PostAsync(serverConfig.UrlTo("accounts"), content).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<RegisterResult>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            return result;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return new LoginResult
                {
                    Successful = false,
                    Error = $"{nameof(loginModel)} is null",
                };
            }
            
            var result = await httpClient.PostJsonAsync<LoginResult>(serverConfig.UrlTo("login"), loginModel).ConfigureAwait(false);

            if (result.Successful)
            {
                await localStorage.SetItemAsync("authToken", result.Token).ConfigureAwait(false);
                ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            }

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken").ConfigureAwait(false);
            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
