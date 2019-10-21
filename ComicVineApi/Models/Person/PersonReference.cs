using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PersonReference : ComicVineObject
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
