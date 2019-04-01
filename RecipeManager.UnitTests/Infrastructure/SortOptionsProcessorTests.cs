// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortOptionsProcessorTests.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Tests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.UnitTests.Infrastructure
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using RecipeManager.Infrastructure;

    public class SortOptionsProcessorTests
    {
        [Test]
        public void GetAllTerms_NullOrderBy_ShouldReturnEmptyList()
        {
            // Arrange
            var processor = new SortOptionsProcessor<string, int>(null);  // Types don't matter for this

            // Act
            var terms = processor.GetAllTerms();

            // Assert
            Assert.IsEmpty(terms, "Search terms are not empty");
        }

        [Test]
        public void GetAllTerms_NoSpaces_ShouldAssignToNames()
        {
            // Arrange
            var elements = new[] { "abc", "def", "G_hi_jkl", "m_desc" };
            var processor = new SortOptionsProcessor<int, bool>(elements);

            // Act
            var terms = processor.GetAllTerms().ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(elements.Length, terms.Length, "Search terms does not have expected number of elements");
                foreach (var (element, term) in elements.Zip(terms, Tuple.Create))
                {
                    Assert.AreEqual(element, term.Name, "Unexpected name");
                }
            });
        }
    }
}