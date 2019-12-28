namespace RecipeManager.ApplicationCore.Resources
{
    public class RootResponse : BaseResource
    {
        public Link? Recipes { get; set; }

        public Link? Users { get; set; }
    }
}
