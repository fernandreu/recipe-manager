namespace RecipeManager.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using RecipeManager.Extensions;
    using RecipeManager.Models;

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

            var result = new IngredientSearchTerm();

            // First, check if the first token is any recognized search operator for this
            var parts = criteria.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            foreach (var op in ValidSearchOperators)
            {
                if (!parts[0].Is(op))
                {
                    continue;
                }

                result.Operator = op;
                parts = parts[1].Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                break;
            }
            
            var quantityAndUnits = parts[0];
            if (!char.IsDigit(quantityAndUnits[0]))
            {
                // Simpler case: no quantity specified
                result.Name = string.Join(' ', parts);
                return result;
            }

            if (parts.Length != 2)
            {
                // We need at least two tokens, e.g. '3 eggs'. Queries such as '3' do not make sense
                return null;
            }

            result.Name = parts[1];

            // Parse first part
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

            if (index < parts[0].Length)
            {
                result.Units = parts[0].Substring(index);
            }

            return result;
        }
    }
}
