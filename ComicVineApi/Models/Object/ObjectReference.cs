using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ObjectReference : ComicVineObject
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
