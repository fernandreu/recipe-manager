// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerConfigTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 28/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FluentAssertions;
using RecipeManager.ApplicationCore.Models;
using Xunit;

namespace RecipeManager.UnitTests.Client
{
    public class ServerConfigTests
    {
        [Theory]
        [InlineData("https://an.example.com", 5000, "recipes", "https://an.example.com:5000/recipes")]
        public void UrlTo_ProducesExpectedResult(string baseUrl, int port, string endpoint, string expected)
        {
            // Arrange
            var config = new ServerConfig(new Uri(baseUrl), port);
            
            // Act
            var url = config.UrlTo(endpoint).ToString();
            
            // Assert
            url.Should().Be(expected);
        }
    }
}