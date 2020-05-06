﻿using System;
using System.Net.Http;
 using System.Net.Http.Headers;
 using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RecipeManager.ApplicationCore.Models;
using WebClient.Login;
using WebClient.Models;
using WebClient.Services;

namespace WebClient
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<UserSession>();
            builder.Services.AddSingleton<ServerConfig>();

            await builder.Build().RunAsync().ConfigureAwait(false);
        }
    }
}
