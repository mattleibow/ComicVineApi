using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SeriesCharacterReference : CharacterReference
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}
