﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchOptionsProcessorTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 01/04/2019.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.UnitTests.Infrastructure
{
    using System;
    using System.Linq;

    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Search;

    using Xunit;

    public class SearchOptionsProcessorTests
    {
        [Fact]
        public void GetAllTerms_NoSpaces_ShouldAssignToNames()
        {
            // Arrange
            var elements = new[] { "abc", "def", "G_hi_jkl", "m_desc" };
            var processor = new SearchOptionsProcessor<Recipe>(elements);

            // Act
            var terms = processor.GetAllTerms().ToArray();

            // Assert
            Assert.Equal(elements.Length, terms.Length);
            foreach (var (element, term) in elements.Zip(terms, Tuple.Create))
            {
                Assert.Equal(element, term.Name);
                Assert.Null(term.Operator);
                Assert.Null(term.Value);
                Assert.False(term.ValidSyntax, "Syntax is actually valid");
            }
        }

        [Fact]
        public void GetAllTerms_NullSearchQuery_ShouldReturnEmptyList()
        {
            // Arrange
            var processor = new SearchOptionsProcessor<Recipe>(null);

            // Act
            var terms = processor.GetAllTerms();

            // Assert
            Assert.Empty(terms);
        }
    }
}