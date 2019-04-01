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
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RecipeManager.Models;

    using Xunit;

    public class RecipesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public RecipesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetRecipes()
        {
            // Act
            var httpResponse = await this.client.GetAsync("/api/recipes");
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<PagedCollection<Recipe>>(stringResponse);
            Assert.Contains(recipes.Value, r => r.Title.Equals("Spanish Omelet", StringComparison.OrdinalIgnoreCase));
        }
    }
}