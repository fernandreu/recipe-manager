// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the Resource type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Models
{
    using System;

    using Newtonsoft.Json;

    public abstract class Resource : Link
    {
        [JsonProperty(Order = -3, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Link Self { get; set; }
    }
}
