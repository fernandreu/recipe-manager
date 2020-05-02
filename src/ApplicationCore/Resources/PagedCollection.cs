using Newtonsoft.Json;

namespace RecipeManager.ApplicationCore.Resources
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        public int Size { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link? First { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link? Previous { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link? Next { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link? Last { get; set; }
    }
}
