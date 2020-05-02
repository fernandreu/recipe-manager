namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface IIngredient
    {
        string Name { get; set; }

        double Quantity { get; set; }

        string? Units { get; set; }

        /// <summary>
        /// Gets a combination of quantity, units and name of this ingredient (e.g. "1kg rice")
        /// </summary>
        /// The explicit definition of FullName as a property (and not just the extension method) means it will be
        /// automatically shown when returned in a controller's method. As it is get-only, Entity Framework will not
        /// store it in the database even if it was defined in the Ingredient
        public string? FullName
        {
            get
            {
                if (Units == null)
                {
                    return $"{Quantity} {Name}";
                }

                return $"{Quantity}{Units} {Name}";
            }
        }
    }
}
