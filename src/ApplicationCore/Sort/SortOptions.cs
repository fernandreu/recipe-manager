namespace RecipeManager.ApplicationCore.Sort
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RecipeManager.ApplicationCore.Entities;

    public class SortOptions<T> : IValidatableObject where T : BaseEntity
    {
        public string[] OrderBy { get; set; }

        // ASP.NET Core calls this to validate incoming parameters
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T>(this.OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);

            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult($"Invalid sort term '{term}'", new[] { nameof(this.OrderBy) });
            }
        }

        // The service code will call this to apply these sort options to a database query
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var processor = new SortOptionsProcessor<T>(this.OrderBy);
            return processor.Apply(query);
        }
    }
}
