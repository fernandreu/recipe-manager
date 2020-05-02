// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortOptionsProcessorTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 01/04/2019.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using RecipeManager.ApplicationCore.Sort;
using RecipeManager.Infrastructure.Entities;
using Xunit;

namespace RecipeManager.UnitTests.Infrastructure
{
    public class SortOptionsProcessorTests
    {
        [Fact]
        public void GetAllTerms_NoSpaces_ShouldAssignToNames()
        {
            // Arrange
            var elements = new[] { "abc", "def", "G_hi_jkl", "m_desc" };
            var processor = new SortOptionsProcessor<Recipe>(elements);

            // Act
            var terms = processor.GetAllTerms().ToArray();

            // Assert
            Assert.Equal(elements.Length, terms.Length);
            foreach (var (element, term) in elements.Zip(terms, Tuple.Create))
            {
                Assert.Equal(element, term.Name);
            }
        }

        [Fact]
        public void GetAllTerms_NullOrderBy_ShouldReturnEmptyList()
        {
            // Arrange
            var processor = new SortOptionsProcessor<Recipe>(null); // Types don't matter for this

            // Act
            var terms = processor.GetAllTerms();

            // Assert
            Assert.Empty(terms);
        }
    }
}