using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RecipeManager.ApplicationCore.Resources
{
    public class Link
    {
        public const string GetMethod = "GET";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public string? Href { get; set; }

        [JsonPropertyName("rel")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public IReadOnlyCollection<string>? Relations { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; } = GetMethod;

        // Stores the route name before being rewritten by the LinkRewritingFilter
        [JsonIgnore]
        public string RouteName { get; set; } = string.Empty;
        
        // Stores the route parameters before being rewritten by the LinkRewritingFilter
        [JsonIgnore]
        public object? RouteValues { get; set; }

        public static Link To(string routeName, object? routeValues = null) =>
            new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = null,
            };

        public static Link ToCollection(string routeName, object? routeValues = null) =>
            new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = new[] { "Collection" }
            };
    }
}
