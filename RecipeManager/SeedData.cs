// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeedData.cs" company="MasterCHefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SeedData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.DependencyInjection;

    using RecipeManager.Models;

    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<RecipeApiDbContext>());
        }

        public static async Task AddTestData(RecipeApiDbContext context)
        {
            if (context.Recipes.Any())
            {
                // Already has data
                return;
            }
            
            context.Recipes.Add(new RecipeEntity
            {
                Id = Guid.NewGuid(),
                Title = "Spanish Omelet",
                Ingredients = new[]
                {
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 6,
                        Name = "eggs",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Units = "kg",
                        Name = "potatoes",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "onion",
                    },
                },
                Details = "Peel and slice the potatoes and fry them in a saucepan together with mixed eggs.",
            });

            context.Recipes.Add(new RecipeEntity
            {
                Id = Guid.NewGuid(),
                Title = "French Omelet",
                Ingredients = new[]
                {
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 3,
                        Name = "eggs",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "salt",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "oil",
                    },
                },
                Details = "Beat the eggs, put them in a pan with a bit of oil and a pinch of salt, and wait.",
            });

            context.Recipes.Add(new RecipeEntity
            {
                Id = Guid.NewGuid(),
                Title = "Carrot Cake",
                Ingredients = new[]
                {
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 6,
                        Name = "carrots",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 500,
                        Units = "g",
                        Name = "Muscovado sugar",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "flour",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Name = "eggs",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });

            context.Recipes.Add(new RecipeEntity
            {
                Id = Guid.NewGuid(),
                Title = "Spinach and ricotta cannelloni",
                Ingredients = new[]
                {
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 500,
                        Units = "g",
                        Name = "spinach",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "ricotta",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 12,
                        Name = "cannelloni tubes",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 300,
                        Units = "g",
                        Name = "mozzarella",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });
            
            context.Recipes.Add(new RecipeEntity
            {
                Id = Guid.NewGuid(),
                Title = "Vegetarian Fajitas",
                Ingredients = new[]
                {
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Units = "tbsp",
                        Name = "vegetable oil",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "onion, chopped",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "red chilli, seeds removed and chopped",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        Name = "garlic clove, chopped",
                    },
                    new IngredientEntity
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
                context.Recipes.Add(new RecipeEntity
                {
                    Id = Guid.NewGuid(),
                    Title = $"Fake Recipe {i}",
                    Ingredients = new[]
                    {
                        new IngredientEntity
                        {
                            Id = Guid.NewGuid(),
                            Quantity = i,
                            Name = "eggs",
                        },
                        new IngredientEntity
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

            await context.SaveChangesAsync();
        }
    }
}
