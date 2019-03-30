// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Collection(T).cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Collection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Models
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
