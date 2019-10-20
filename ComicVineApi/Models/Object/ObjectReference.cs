using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ObjectReference : Reference
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
