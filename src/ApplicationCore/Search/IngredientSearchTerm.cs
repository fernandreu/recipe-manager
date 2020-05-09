using System;
using System.Collections.Generic;
using System.Linq;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using UnitsNet;

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

        public string Name { get; set; } = string.Empty;

        public string? Units { get; set; }

        public static IngredientSearchTerm? Parse(string? criteria)
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
            string? quantityAndUnits = null;
            if (parts.Length > 2)
            {
                foreach (var op in ValidSearchOperators)
                {
                    if (!parts[^2].Is(op))
                    {
                        continue;
                    }

                    quantityAndUnits = parts[^1];
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

        public bool IsMatch(IIngredient ingredient)
        {
            return ingredient == null || IsMatch(ingredient.Name, ingredient.Quantity, ingredient.Units);
        }

        public bool IsMatch(string name, double quantity, string? units)
        {
            if (name?.Contains(Name, StringComparison.OrdinalIgnoreCase) != true)
            {
                return false;
            }

            if (Quantity == null)
            {
                // Quantity does not need to be checked then
                return true;
            }
            
            // Check units first, converting the quantity if necessary
            if (Units == null ^ units == null)
            {
                // Either the search was done thinking this was an uncountable ingredient, or the other way
                // around. In either case, there is no right unit to assume
                return false;
            }

            var termQuantity = Quantity.Value;
            if (Units != null && units != null && !TryConvertUnits(Units, units, ref termQuantity))
            {
                return false;
            }

            const double tolerance = 1e-2;

            if (Operator == SearchOperator.LessThan)
            {
                return quantity < termQuantity - tolerance;
            }
            
            if (Operator == SearchOperator.LessThanOrEqual)
            {
                return quantity <= termQuantity + tolerance;
            }
            
            if (Operator == SearchOperator.Equal)
            {
                return Math.Abs(termQuantity - quantity) < tolerance;
            }

            if (Operator == SearchOperator.GreaterThanOrEqual)
            {
                return quantity >= termQuantity - tolerance;
            }
            
            if (Operator == SearchOperator.GreaterThan)
            {
                return quantity > termQuantity + tolerance;
            }
            
            // Unrecognized operator
            return false;
        }
        
        private static bool TryConvertUnits(string unitsFrom, string unitsTo, ref double result)
        {
            foreach (var quantityName in new[] { "Mass", "Volume" })
            {
                try
                {
                    result = UnitConverter.ConvertByAbbreviation(result, quantityName, unitsFrom, unitsTo);
                    return true;
                }
                catch (UnitNotFoundException)
                {
                }
            }

            return false;
        }
    }
}
