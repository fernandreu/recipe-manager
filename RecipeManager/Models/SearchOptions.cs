// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchOptions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RecipeManager.Infrastructure;

    public class SearchOptions<T, TEntity> : IValidatableObject
    {
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(this.Search);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid search term '{term}'", new[] { nameof(this.Search) });
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(this.Search);
            return processor.Apply(query);
        }
    }
}
