using System;
using Newtonsoft.Json;

namespace WebClient.Resources
{
    public abstract class BaseResource : Link
    {
        [JsonProperty(Order = -3, DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Link Self { get; set; }
    }
}
