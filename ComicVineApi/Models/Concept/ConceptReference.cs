using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ConceptReference : Reference
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
