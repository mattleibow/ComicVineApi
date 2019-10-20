using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PersonReference : Reference
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
