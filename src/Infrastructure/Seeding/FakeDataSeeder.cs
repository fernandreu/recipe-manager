// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDataSeeder.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.Infrastructure.Seeding
{
    public class FakeDataSeeder : EmptyDataSeeder
    {
        public const string FakeAdminName = "FakeAdmin";
        
        protected override void Seed()
        {
            Add(new Recipe
            {
                Title = "Spanish Omelet",
                Ingredients = new[]
                {
                    new RecipeIngredient
                    {
                        Quantity = 6,
                        Name = "eggs",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 2,
                        Units = "kg",
                        Name = "potatoes",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Name = "onion",
                    },
                },
                Details = "Peel and slice the potatoes and fry them in a saucepan together with mixed eggs.",
            });
            
            Add(new Recipe
            {
                Title = "French Omelet",
                Ingredients = new[]
                {
                    new RecipeIngredient
                    {
                        Quantity = 3,
                        Name = "eggs",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "salt",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Units = "tbsp",
                        Name = "oil",
                    },
                },
                Details = "Beat the eggs, put them in a pan with a bit of oil and a pinch of salt, and wait.",
            });
            
            Add(new Recipe
            {
                Title = "Carrot Cake",
                Ingredients = new[]
                {
                    new RecipeIngredient
                    {
                        Quantity = 6,
                        Name = "carrots",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 500,
                        Units = "g",
                        Name = "Muscovado sugar",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 300,
                        Units = "g",
                        Name = "flour",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 2,
                        Name = "eggs",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });
            
            Add(new Recipe
            {
                Title = "Spinach and ricotta cannelloni",
                Ingredients = new[]
                {
                    new RecipeIngredient
                    {
                        Quantity = 500,
                        Units = "g",
                        Name = "spinach",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 300,
                        Units = "g",
                        Name = "ricotta",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 12,
                        Name = "cannelloni tubes",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 300,
                        Units = "g",
                        Name = "mozzarella",
                    },
                },
                Details = "Mix everything together and put it into the oven somehow.",
            });
            
            Add(new Recipe
            {
                Title = "Vegetarian Fajitas",
                Ingredients = new[]
                {
                    new RecipeIngredient
                    {
                        Quantity = 2,
                        Units = "tbsp",
                        Name = "vegetable oil",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Name = "onion, chopped",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Name = "red chilli, seeds removed and chopped",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 1,
                        Name = "garlic clove, chopped",
                    },
                    new RecipeIngredient
                    {
                        Quantity = 400,
                        Units = "g",
                        Name = "tin kidney beans, drained and rinsed",
                    },
                },
                Details = "Check: https://www.bbc.com/food/recipes/fajitas_8651",
            });
            
            for (var i = 1; i < 50; ++i)
            {
                Add(new Recipe
                {
                    Title = $"Fake Recipe {i}",
                    Ingredients = new[]
                    {
                        new RecipeIngredient
                        {
                            Quantity = i,
                            Name = "eggs",
                        },
                        new RecipeIngredient
                        {
                            Quantity = i * 25 * (i % 2 == 0 ? 0.001 : 1),
                            Units = i % 2 == 0 ? "kg" : "g",
                            Name = "sugar",
                        },
                    },
                    Details = "This is just for testing purposes",
                });
            }
            
            var user = new ApplicationUser
            {
                UserName = FakeAdminName,
                Email = "fake.admin@andreu.info",
                SecurityStamp = Guid.NewGuid().ToString(),
                Ingredients = new[]
                {
                    new UserIngredient
                    {
                        Quantity = 5,
                        Name = "eggs",
                    },
                    new UserIngredient
                    {
                        Quantity = 2,
                        Units = "kg",
                        Name = "sugar",
                    },
                }
            };
            Add(user);
        }
    }
}