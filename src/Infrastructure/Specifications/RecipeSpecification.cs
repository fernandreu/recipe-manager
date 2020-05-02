using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.Infrastructure.Specifications
{
    public class RecipeSpecification : Specification<Recipe>
    {
        public RecipeSpecification(SpecificationOptions<Recipe> options)
        {
            ApplyOptions(options);
        }
    }
}
