namespace RecipeManager.Web.Resources
{
    using System;

    using Newtonsoft.Json;

    public abstract class BaseResource : Link
    {
        [JsonProperty(Order = -3, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Link Self { get; set; }
    }
}
