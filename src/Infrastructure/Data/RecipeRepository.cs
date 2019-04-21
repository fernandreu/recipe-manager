namespace RecipeManager.Infrastructure.Data
{
    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Interfaces;

    public class RecipeRepository : EfRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeApiDbContext context)
            : base(context)
        {
        }
    }
}
