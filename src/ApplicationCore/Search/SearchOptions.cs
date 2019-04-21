namespace RecipeManager.ApplicationCore.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RecipeManager.ApplicationCore.Entities;

    public class SearchOptions<T> : IValidatableObject where T : BaseEntity
    {
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T>(this.Search);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid search term '{term}'", new[] { nameof(this.Search) });
            }
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var processor = new SearchOptionsProcessor<T>(this.Search);
            return processor.Apply(query);
        }
    }
}
