using System.Text.Json.Serialization;

namespace RecipeManager.ApplicationCore.Resources
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public int? Offset { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public int? Limit { get; set; }

        public int Size { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public Link? First { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public Link? Previous { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public Link? Next { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]
        public Link? Last { get; set; }
    }
}
