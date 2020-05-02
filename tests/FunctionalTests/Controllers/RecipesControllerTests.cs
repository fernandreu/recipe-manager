// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecipesControllerTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 01/04/2019.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using RecipeManager.ApplicationCore.Resources;
using RecipeManager.WebApi;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using RecipeManager.ApplicationCore.Search;
using UnitsNet;

using Xunit;

namespace RecipeManager.FunctionalTests.Controllers
{
    [SuppressMessage("ReSharper", "CA1707", Justification = "Test methods can contain underscores")]
    public class RecipesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public RecipesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllRecipes_NullSearchTerms()
        {
            // Arrange / Act
            var recipes = await TestGetAll();

            // Assert
            Assert.NotEmpty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_SearchByJustIngredientName_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs").ConfigureAwait(false);

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs")));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.LessThan} 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity < 11.99));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanOrEqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.LessThanOrEqual} 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity <= 12.01));
        }

        [Fact]
        public async Task GetAllRecipes_EqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.Equal} 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && Math.Abs(i.Quantity - 12.0) < 1e-2));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanOrEqualToNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.GreaterThanOrEqual} 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity >= 11.99));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanNonDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.GreaterThan} 12");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("eggs") && i.Units == null && i.Quantity > 12.01));
        }

        [Fact]
        public async Task GetAllRecipes_DimInconsistency_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.LessThan} 12kg");

            // Assert
            Assert.Empty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_UnitsInconsistency_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.GreaterThan} 12tbsp");

            // Assert
            Assert.Empty(recipes.Value);
        }

        [Fact]
        public async Task GetAllRecipes_LessThanDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.LessThan} 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") < 499.99));
        }

        [Fact]
        public async Task GetAllRecipes_LessThanOrEqualToDim_ShouldReturnEmptyList()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.LessThanOrEqual} 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") <= 500.01));
        }

        [Fact]
        public async Task GetAllRecipes_EqualToDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.Equal} 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && Math.Abs(UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") - 500) <= 1e-2));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanOrEqualToDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.GreaterThanOrEqual} 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") >= 499.99));
        }

        [Fact]
        public async Task GetAllRecipes_GreaterThanDim_ShouldFindRecipes()
        {
            // Arrange / Act
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.GreaterThan} 0.5kg");

            // Assert
            Assert.NotEmpty(recipes.Value);
            Assert.All(recipes.Value, r => Assert.Contains(r.Ingredients, i => i.Name.Contains("sugar") && UnitConverter.ConvertByAbbreviation(i.Quantity, "Mass", i.Units, "g") > 500.01));
        }
        
        [Fact]
        public async Task GetAllRecipes_MultipleQueries_ShouldFindRecipes()
        {
            // Arrange / Act (note: due to how seed data is generated, not all search terms will actually yield non-empty results)
            var recipes = await TestGetAll($"search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} sugar {SearchOperator.GreaterThan} 0.3kg&search={nameof(RecipeResource.Ingredients)} {SearchOperator.Contains} eggs {SearchOperator.LessThan} 40");

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

        private async Task<PagedCollection<RecipeResource>> TestGetAll(string query = null)
        {
            Console.WriteLine($"Query: {query}");
            var httpResponse = await client.GetAsync("/recipes" + (query != null ? $"?{query}" : string.Empty));
            
            Console.WriteLine($"Status code: {httpResponse.StatusCode}");

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK, $"it should not be {(int) httpResponse.StatusCode} ({httpResponse.StatusCode}): {stringResponse}");
            
            return JsonConvert.DeserializeObject<PagedCollection<RecipeResource>>(stringResponse);
        }
    }
}