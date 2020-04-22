using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.Infrastructure.Seeding;

namespace RecipeManager.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
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
        
        // TODO: Should Users (from identity services) and AppUsers be the same? I.e. derive from IdentityUser and add our custom properties
        public DbSet<User> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            dataSeeder.SeedData(builder);
        }
    }
}
