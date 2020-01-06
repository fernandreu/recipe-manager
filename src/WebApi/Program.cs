using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RecipeManager.Infrastructure.Data;

namespace RecipeManager.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            InitializeDatabase(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void InitializeDatabase(IWebHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            
            try
            {
                DbContextSeed.InitializeAsync(services).Wait();
            }
            catch (Exception ex) // TODO: Need to find which exception might this actually throw
            {
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                var localizer = services.GetRequiredService<IStringLocalizer<ApplicationDbContext>>();
                logger.LogError(ex, localizer["An error occurred seeding the database."]);
            }
        }
    }
}