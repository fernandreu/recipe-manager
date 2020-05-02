﻿using System;
using System.Collections.Generic;
using System.Linq;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Search;
using UnitsNet;

namespace RecipeManager.ApplicationCore.Extensions
{
    /// <summary>
    /// Defines all instance methods that are common to both ingredient resources and ingredient entities
    /// </summary>
    public static class IngredientExtensions
    {
        public static bool IsMatch<T>(this T ingredient, IngredientSearchTerm searchTerm)
            where T : IIngredient
        {
            if (ingredient == null || searchTerm == null)
            {
                return false;
            }

            if (!ingredient.Name.Contains(searchTerm.Name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (searchTerm.Quantity == null)
            {
                // Quantity does not need to be checked then
                return true;
            }
            
            // Check units first, converting the quantity if necessary
            if (searchTerm.Units == null ^ ingredient.Units == null)
            {
                // Either the search was done thinking this was an uncountable ingredient, or the other way
                // around. In either case, there is no right unit to assume
                return false;
            }

            var quantity = searchTerm.Quantity.Value;
            if (searchTerm.Units != null && ingredient.Units != null && !TryConvertUnits(searchTerm.Units, ingredient.Units, ref quantity))
            {
                return false;
            }

            const double tolerance = 1e-2;

            if (searchTerm.Operator == SearchOperator.LessThan)
            {
                return ingredient.Quantity < quantity - tolerance;
            }
            
            if (searchTerm.Operator == SearchOperator.LessThanOrEqual)
            {
                return ingredient.Quantity <= quantity + tolerance;
            }
            
            if (searchTerm.Operator == SearchOperator.Equal)
            {
                return Math.Abs(quantity - ingredient.Quantity) < tolerance;
            }

            if (searchTerm.Operator == SearchOperator.GreaterThanOrEqual)
            {
                return ingredient.Quantity >= quantity - tolerance;
            }
            
            if (searchTerm.Operator == SearchOperator.GreaterThan)
            {
                return ingredient.Quantity > quantity + tolerance;
            }
            
            // Unrecognized operator
            return false;
        }

        public static bool IsMatch<T>(this IEnumerable<T> ingredients, string query)
            where T : IIngredient
        {
            if (ingredients == null)
            {
                return false;
            }
            
            var searchTerm = IngredientSearchTerm.Parse(query);
            if (searchTerm == null)
            {
                // TODO: Perhaps this should raise an exception
                return false;
            }

            return ingredients.Any(x => x.IsMatch(searchTerm));
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
