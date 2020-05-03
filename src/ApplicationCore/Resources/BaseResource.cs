using System;
using System.Text.Json.Serialization;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Resources
{
    public abstract class BaseResource : Link, ISingleEntity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Link? Self { get; set; }
    }
}
