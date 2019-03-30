// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortOptions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SortOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RecipeManager.Infrastructure;

    public class SortOptions<T, TEntity> : IValidatableObject
    {
        public string[] OrderBy { get; set; }

        // ASP.NET Core calls this to validate incoming parameters
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(this.OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid sort term '{term}'", new[] { nameof(this.OrderBy) });
            }
        }

        // The service code will call this to apply these sort options to a database query
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(this.OrderBy);
            return processor.Apply(query);
        }
    }
}
