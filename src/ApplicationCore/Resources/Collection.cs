namespace RecipeManager.ApplicationCore.Resources
{
    public class Collection<T> : BaseResource
    {
        public T[] Value { get; set; } = new T[0];
    }
}
