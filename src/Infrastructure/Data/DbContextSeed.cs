namespace RecipeManager.Infrastructure.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using RecipeManager.ApplicationCore.Entities;

    public static class DbContextSeed
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<ApplicationDbContext>());
        }

        public static async Task AddTestData(ApplicationDbContext context)
        {
            if (context.Recipes.Any())
            {
                // Already has data
                return;
            }
            
            context.Recipes.Add(new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Spanish Omelet",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 6,
                        Name = "eggs",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Units = "kg",
                        Name = "potatoes",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "onion",
                    },
                },
                Details = "Peel and slice the potatoes and fry them in a saucepan together with mixed eggs.",
            });

            context.Recipes.Add(new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "French Omelet",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 3,
                        Name = "eggs",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "salt",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "oil",
                    },
                },
                Details = "Beat the eggs, put them in a pan with a bit of oil and a pinch of salt, and wait.",
            });

            context.Recipes.Add(new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Carrot Cake",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 6,
                        Name = "carrots",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 500,
                        Units = "g",
                        Name = "Muscovado sugar",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "flour",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Name = "eggs",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });

            context.Recipes.Add(new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Spinach and ricotta cannelloni",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 500,
                        Units = "g",
                        Name = "spinach",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "ricotta",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 12,
                        Name = "cannelloni tubes",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "mozzarella",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });
            
            context.Recipes.Add(new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Vegetarian Fajitas",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Units = "tbsp",
                        Name = "vegetable oil",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "onion, chopped",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "red chilli, seeds removed and chopped",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "garlic clove, chopped",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 400,
                        Units = "g",
                        Name = "tin kidney beans, drained and rinsed",
                    },
                },
                Details = "Check: https://www.bbc.com/food/recipes/fajitas_8651",
            });

            for (var i = 1; i < 50; ++i)
            {
                context.Recipes.Add(new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = $"Fake Recipe {i}",
                    Ingredients = new[]
                    {
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Quantity = i,
                            Name = "eggs",
                        },
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Quantity = i * 25 * (i % 2 == 0 ? 0.001 : 1),
                            Units = i % 2 == 0 ? "kg" : "g",
                            Name = "sugar",
                        },
                    },
                    Details = "This is just for testing purposes",
                });
            }

            context.AppUsers.Add(new User
            {
                Id = Guid.NewGuid(),
                UserName = "fernandreu",
                Ingredients = new[]
                {
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 50,
                        Name = "eggs",
                    },
                    new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 200,
                        Units = "kg",
                        Name = "sugar",
                    },
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
