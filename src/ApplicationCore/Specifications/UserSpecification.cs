using RecipeManager.ApplicationCore.Entities;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class UserSpecification : Specification<User>
    {
        public UserSpecification(SpecificationOptions<User> options)
        {
            this.ApplyOptions(options);
        }
    }
}
