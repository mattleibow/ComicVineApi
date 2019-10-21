using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PersonCredit : ComicVineObject
    {
        [JsonProperty("role")]
        public string? Role { get; set; }
    }
}
