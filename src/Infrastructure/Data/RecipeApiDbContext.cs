using Microsoft.EntityFrameworkCore;

namespace RecipeManager.Infrastructure.Data
{
    using RecipeManager.ApplicationCore.Entities;

    public class RecipeApiDbContext : DbContext
    {
        public RecipeApiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
