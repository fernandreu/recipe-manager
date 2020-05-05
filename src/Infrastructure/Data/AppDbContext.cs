﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManager.Infrastructure.Entities;
using RecipeManager.Infrastructure.Seeding;

namespace RecipeManager.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IDataSeeder dataSeeder;
        
        public AppDbContext(DbContextOptions options, IDataSeeder dataSeeder)
            : base(options)
        {
            this.dataSeeder = dataSeeder;
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<UserIngredient> UserIngredients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            dataSeeder.SeedData(builder);
        }
    }
}
