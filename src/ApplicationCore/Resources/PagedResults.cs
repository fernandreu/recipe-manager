using System.Collections.Generic;
using System.Linq;

namespace RecipeManager.ApplicationCore.Resources
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        public int TotalSize { get; set; }
    }
}
