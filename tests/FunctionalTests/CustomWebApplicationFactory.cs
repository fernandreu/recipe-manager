// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomWebApplicationFactory.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 01/04/2019.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecipeManager.Infrastructure.Data;

namespace RecipeManager.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                // Add a database Context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<AppDbContext>(options =>
                {
                    // In-memory database does not cause errors due to client-side code being executed on the server.
                    // Hence, we have to use a full-blown PostgreSQL database
                    options.UseNpgsql("Host=localhost;Port=5432;Database=recipemanager-tests;Username=postgres;Password=SuperSecure");
                });

                // Build the service provider
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                
                var appDb = scopedServices.GetRequiredService<AppDbContext>();

                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created from scratch.
                appDb.Database.EnsureDeleted();
                appDb.Database.EnsureCreated();
            });
        }
    }
}