using System.Collections.Generic;
using System.Linq;

namespace RecipeManager.ApplicationCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count = 1)
        {
            var array = source as T[] ?? source.ToArray();
            return array.Take(array.Length - count);
        }
    }
}
