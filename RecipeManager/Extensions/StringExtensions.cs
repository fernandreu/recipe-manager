using System;

using RecipeManager.Models;

namespace RecipeManager.Extensions
{
    public static class StringExtensions
    {
        public static bool Is(this string s, SearchOperator op)
        {
            return s.Equals(op.Value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
