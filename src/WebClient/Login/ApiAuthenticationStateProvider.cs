using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RecipeManager.ApplicationCore.Models;

namespace WebClient.Login
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        
        private readonly ILocalStorageService localStorage;

        private readonly ServerConfig serverConfig;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage, ServerConfig serverConfig)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.serverConfig = serverConfig;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await localStorage.GetItemAsync<string>("authToken").ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(savedToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
            }

            var userInfo = await httpClient.GetJsonAsync<UserModel>( serverConfig.UrlTo("accounts/user").ToString()).ConfigureAwait(false);

            var identity = userInfo.IsAuthenticated
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.UserName) }, "apiauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
