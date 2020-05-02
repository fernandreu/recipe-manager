namespace RecipeManager.ApplicationCore.Sort
{
    public class SortTerm
    {
        public string Name { get; set; } = string.Empty;

        public string? EntityName { get; set; }

        public bool Descending { get; set; }

        public bool Default { get; set; }
    }
}
