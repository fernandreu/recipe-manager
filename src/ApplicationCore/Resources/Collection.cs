using System.Collections.Generic;

namespace RecipeManager.ApplicationCore.Resources
{
    public class Collection<T> : BaseResource
    {
        public IReadOnlyCollection<T>? Value { get; set; }
    }
}
