namespace RecipeManager.WebApi.Resources
{
    public class Collection<T> : BaseResource
    {
        public T[] Value { get; set; }
    }
}
