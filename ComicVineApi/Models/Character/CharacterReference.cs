using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterReference : Reference
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
