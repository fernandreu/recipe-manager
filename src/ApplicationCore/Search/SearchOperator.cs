using System;

namespace RecipeManager.ApplicationCore.Search
{
    /// <summary>
    /// Contains all possible operators that can be used in a search query
    /// </summary>
    public class SearchOperator
    {
        public static readonly SearchOperator LessThan = new SearchOperator("lt");
        
        public static readonly SearchOperator LessThanOrEqual = new SearchOperator("le");

        public static readonly SearchOperator Equal = new SearchOperator("eq");

        public static readonly SearchOperator GreaterThanOrEqual = new SearchOperator("ge");

        public static readonly SearchOperator GreaterThan = new SearchOperator("gt");

        public static readonly SearchOperator Contains = new SearchOperator("co");

        private SearchOperator(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool Is(string op)
        {
            return Value.Equals(op, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
