using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class SpecificationOptions<T> : IValidatableObject
        where T : BaseEntity
    {
        public string[] Search { get; set; }

        public string[] OrderBy { get; set; }
        
        public PagingOptions Paging { get;set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidateSearchOptions(validationContext).Concat(this.ValidateSortOptions(validationContext));
        }

        private IEnumerable<ValidationResult> ValidateSearchOptions(ValidationContext validationContext)
        {
            if (this.Search == null)
            {
                yield break;
            }

            var processor = new SearchOptionsProcessor<T>(this.Search);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid search term '{term}'", new[] { nameof(this.Search) });
            }
        }

        private IEnumerable<ValidationResult> ValidateSortOptions(ValidationContext validationContext)
        {
            if (this.OrderBy == null)
            {
                yield break;
            }

            var processor = new SortOptionsProcessor<T>(this.OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid sort term '{term}'", new[] { nameof(this.OrderBy) });
            }
        }
        
    }
}
