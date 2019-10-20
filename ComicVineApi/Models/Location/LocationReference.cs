using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class LocationReference : Reference
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
