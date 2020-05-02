// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 02/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using RecipeManager.Infrastructure.Search;

namespace RecipeManager.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool Is(this string s, SearchOperator op)
        {
            return s?.Equals(op?.Value, StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}