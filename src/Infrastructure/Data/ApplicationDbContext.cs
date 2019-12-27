using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;

namespace RecipeManager.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        // TODO: Should Users (from identity services) and AppUsers be the same? I.e. derive from IdentityUser and add our custom properties
        public DbSet<User> AppUsers { get; set; }
    }
}
