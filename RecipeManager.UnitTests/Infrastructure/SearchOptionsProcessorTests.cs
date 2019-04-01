// --------------------------------------------------------------------------------------------------------------------
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

    using NUnit.Framework;

    using RecipeManager.Infrastructure;

    public class SearchOptionsProcessorTests
    {
        [Test]
        public void GetAllTerms_NoSpaces_ShouldAssignToNames()
        {
            // Arrange
            var elements = new[] { "abc", "def", "G_hi_jkl", "m_desc" };
            var processor = new SearchOptionsProcessor<string, int>(elements);

            // Act
            var terms = processor.GetAllTerms().ToArray();

            // Assert
            Assert.AreEqual(elements.Length, terms.Length, "Search terms does not have expected number of elements");
            Assert.Multiple(() =>
            {
                foreach (var (element, term) in elements.Zip(terms, Tuple.Create))
                {
                    Assert.AreEqual(element, term.Name, "Unexpected name");
                    Assert.IsNull(term.Operator, "Operator is not empty");
                    Assert.IsNull(term.Value, "Value is not empty");
                    Assert.IsFalse(term.ValidSyntax, "Syntax is actually valid");
                }
            });
        }

        [Test]
        public void GetAllTerms_NullOrderBy_ShouldReturnEmptyList()
        {
            // Arrange
            var processor = new SearchOptionsProcessor<bool, short>(null);

            // Act
            var terms = processor.GetAllTerms();

            // Assert
            Assert.IsEmpty(terms, "Search terms are not empty");
        }
    }
}