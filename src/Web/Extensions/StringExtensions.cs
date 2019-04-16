namespace RecipeManager.Web.Extensions
{
    using System;

    using RecipeManager.Web.Models;

    public static class StringExtensions
    {
        public static bool Is(this string s, SearchOperator op)
        {
            return s.Equals(op.Value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
