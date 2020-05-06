﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesignDbContextFactory.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 25/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RecipeManager.Infrastructure.Seeding;

namespace RecipeManager.Infrastructure.Data
{

    public class DesignDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public AppDbContext CreateDbContext(
            string host = "localhost",
            string database = "recipemanager-migrations",
            string username = "postgres",
            string password = "SuperSecure",
            int port = 5432)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql($"Host={host};Port={port};Database={database};Username={username};Password={password}")
                .Options;
            return new AppDbContext(options, new EmptyDataSeeder());
        }
    }
}