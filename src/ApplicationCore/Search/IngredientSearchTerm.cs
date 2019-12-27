using System;
using System.Collections.Generic;
using System.Linq;
using RecipeManager.ApplicationCore.Extensions;

namespace RecipeManager.ApplicationCore.Search
{
    public class IngredientSearchTerm
    {
        private static readonly IEnumerable<SearchOperator> ValidSearchOperators = new[]
        {
            SearchOperator.LessThan,
            SearchOperator.LessThanOrEqual,
            SearchOperator.Equal,
            SearchOperator.GreaterThanOrEqual,
            SearchOperator.GreaterThan,
        };

        public SearchOperator Operator { get; set; } = SearchOperator.Equal;

        public double? Quantity { get; set; }

        public string Name { get; set; }

        public string Units { get; set; }

        public static IngredientSearchTerm Parse(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
            {
                return null;
            }

            var result = new IngredientSearchTerm
            {
                Name = criteria
            };

            // First, check if the last but one token is any recognized search operator for this
            var parts = criteria.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string quantityAndUnits = null;
            if (parts.Length > 2)
            {
                foreach (var op in ValidSearchOperators)
                {
                    if (!parts[parts.Length - 2].Is(op))
                    {
                        continue;
                    }

                    quantityAndUnits = parts[parts.Length - 1];
                    parts = parts.SkipLast(2).ToArray();
                    result.Name = string.Join(" ", parts);
                    result.Operator = op;
                    break;
                }
            }
            
            if (quantityAndUnits == null)
            {
                // Simpler case: no quantity specified
                return result;
            }

            // Parse first part of quantityAndUnits
            int index;
            for (index = 0; index < quantityAndUnits.Length; ++index)
            {
                var c = quantityAndUnits[index];
                if (c != '.' && !char.IsDigit(c))
                {
                    break;
                }
            }
            
            if (!double.TryParse(quantityAndUnits.Substring(0, index), out var quantity))
            {
                // TODO: Perhaps this should throw an exception instead
                return null;
            }

            result.Quantity = quantity;

            if (index < quantityAndUnits.Length)
            {
                result.Units = quantityAndUnits.Substring(index);
            }

            return result;
        }
    }
}
