namespace RecipeManager.ApplicationCore.Extensions
{
    using System;

    using RecipeManager.ApplicationCore.Search;

    public static class StringExtensions
    {
        public static bool Is(this string s, SearchOperator op)
        {
            return s.Equals(op.Value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
