using System;
using Newtonsoft.Json;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Resources
{
    public abstract class BaseResource : Link, ISingleEntity
    {
        [JsonProperty(Order = -3, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Link? Self { get; set; }
    }
}
