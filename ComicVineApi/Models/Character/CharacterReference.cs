using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterReference : ComicVineObject
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
