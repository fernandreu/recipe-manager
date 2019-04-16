// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortableAttribute.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SortableAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SortableAttribute : Attribute
    {
        public string EntityProperty { get; set; }

        public bool Default { get; set; }
    }
}
