using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ConceptReference : ComicVineObject
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
