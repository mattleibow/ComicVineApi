using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class LocationReference : ComicVineObject
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
