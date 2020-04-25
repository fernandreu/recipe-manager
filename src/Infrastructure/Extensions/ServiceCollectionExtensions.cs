// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 25/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Helpers;
using RecipeManager.Infrastructure.Seeding;

namespace RecipeManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration.GetSection("DatabaseOptions"));

            services.AddTransient<IDataSeeder, FakeDataSeeder>();
            
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptionsMonitor<DbOptions>>().CurrentValue;

                var connectionString = configuration.GetConnectionString("AppDb");
                switch (dbOptions.Type)
                {
                    case "PostgreSQL":
                        options.UseNpgsql(connectionString);
                        break;
                    default:
                        options.UseInMemoryDatabase("recipesdb");
                        break;
                }
            });
            
            return services;
        }
    }
}