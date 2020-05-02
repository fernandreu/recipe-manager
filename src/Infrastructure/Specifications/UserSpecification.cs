using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.Infrastructure.Specifications
{
    public class UserSpecification : Specification<ApplicationUser>
    {
        public UserSpecification(SpecificationOptions<ApplicationUser> options)
        {
            ApplyOptions(options);
        }
    }
}
