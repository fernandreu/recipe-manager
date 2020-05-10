// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IngredientSearchTermTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 10/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using RecipeManager.ApplicationCore.Search;
using Xunit;

namespace RecipeManager.UnitTests.Core
{
    public class IngredientSearchTermTests
    {
        [Theory]
        [InlineData("eggs", 25, null, "eggs", true)]
        [InlineData("Eggs", 25, null, "eggs", true)]
        [InlineData("eggs", 25, null, "Eggs", true)]
        [InlineData("eggs", 25, null, "abcd", false)]
        [InlineData("eggs", 2, null, "eggs eq 1", false)]
        [InlineData("eggs", 2, null, "eggs eq 2", true)]
        [InlineData("eggs", 2, null, "eggs eq 2g", false)]
        [InlineData("eggs", 2, null, "eggs eq 3", false)]
        [InlineData("eggs", 2, null, "eggs lt 3", true)]
        [InlineData("eggs", 2, null, "eggs lt 2", false)]
        [InlineData("eggs", 2, null, "eggs le 2", true)]
        [InlineData("eggs", 2, null, "eggs gt 1", true)]
        [InlineData("eggs", 2, null, "eggs gt 2", false)]
        [InlineData("eggs", 2, null, "eggs ge 2", true)]
        [InlineData("milk", 2, "l", "milk eq 2", false)]
        [InlineData("milk", 2, "l", "milk eq 2l", true)]
        [InlineData("milk", 2, "l", "milk eq 2L", true)]
        [InlineData("milk", 2, "l", "milk eq 2000ml", true)]
        [InlineData("potatoes", 2, "kg", "potatoes gt 1000g", true)]
        public void IsMatchTests(string name, double quantity, string units, string searchTerm, bool expected)
        {
            // Arrange
            var term = IngredientSearchTerm.Parse(searchTerm);
            
            // Act
            var actual = term.IsMatch(name, quantity, units);
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}