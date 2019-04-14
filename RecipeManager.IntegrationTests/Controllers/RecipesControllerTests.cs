// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipesControllerTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 01/04/2019.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.IntegrationTests.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;

    using Newtonsoft.Json;

    using RecipeManager.Extensions;
    using RecipeManager.Models;

    using UnitsNet;

    using Xunit;

    public class RecipesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public RecipesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllRecipes_NullSearchTerms()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll();

            // Assert
            Assert.NotEmpty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_SearchByJustIngredientName_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs")));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs lt 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity < 11.99));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanOrEqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs le 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity <= 12.01));
        }

        [Fact]
        public async Task GetAllRecipes_EqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs eq 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && Math.Abs(i.Quantity - 12.0) < 1e-2));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanOrEqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs ge 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity >= 11.99));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs gt 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity > 12.01));
        }

        [Fact]
        public async Task GetAllRecipes_DimInconsistency_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains eggs lt 12kg");

            // Assert
            Assert.Empty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_UnitsInconsistency_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar gt 12tbsp");

            // Assert
            Assert.Empty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_LessThanDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar lt 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") < 499.99));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanOrEqualToDim_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar le 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") <= 500.01));
        }

        [Fact]
        public async Task GetAllRecipes_EqualToDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar eq 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && Math.Abs(UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") - 500) <= 1e-2));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanOrEqualToDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar ge 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") >= 499.99));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await this.TestGetAll("search=ingredients contains sugar gt 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") > 500.01));
        }
        
        [Fact]
        public async Task GetAllRecipes_MultipleQueries_ShouldFindRecipes()
        {
            // Arrange / Act (note: due to how seed data is generated, not all search terms will actually yield non-empty results)
            var recipes = await this.TestGetAll("search=ingredients contains sugar gt 0.3kg&search=ingredients contains eggs lt 40");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(
                recipes.Value, 
                r =>
                {
                    Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") > 300.01);
                    Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity < 39.99);
                });
        }

        private async Task<PagedCollection<Recipe>> TestGetAll(string query = null)
        {
            var httpResponse = await this.client.GetAsync("/api/recipes" + (query != null ? $"?{query}" : string.Empty));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PagedCollection<Recipe>>(stringResponse);
        }
    }
}