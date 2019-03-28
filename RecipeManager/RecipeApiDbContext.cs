// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipeApiDbContext.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the RecipeApiDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager
{
    using Microsoft.EntityFrameworkCore;

    using RecipeManager.Models;

    public class RecipeApiDbContext : DbContext
    {
        public RecipeApiDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<RecipeEntity> Recipes { get; set; }

        public DbSet<IngredientEntity> Ingredients { get; set; }
    }
}
