using RecipeManager.ApplicationCore.Entities;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(SpecificationOptions<User> options)
        {
            this.ApplyOptions(options);
        }
    }
}
