using RecipeManager.ApplicationCore.Entities;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class RecipeSpecification : Specification<Recipe>
    {
        public RecipeSpecification(SpecificationOptions<Recipe> options)
        {
            this.ApplyOptions(options);
        }
    }
}
