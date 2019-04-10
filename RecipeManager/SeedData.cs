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
                        Name = "6 eggs",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "2kg potatoes",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "1 onion",
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
                        Name = "3 eggs",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Salt",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Oil",
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
                        Name = "6 carrots",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Muscovado sugar",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "300g flour",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "2 eggs",
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
                        Name = "500g spinach",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "300g ricotta",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "12 cannelloni tubes",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "300g mozzarella",
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
                        Name = "2tbsp vegetable oil",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "1 onion, chopped",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "1 red chilli, seeds removed and chopped",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "1 garlic clove, chopped",
                    },
                    new IngredientEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "400g tin kidney beans, drained and rinsed",
                    },
                },
                Details = "Check: https://www.bbc.com/food/recipes/fajitas_8651",
            });

            await context.SaveChangesAsync();
        }
    }
}
