using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PersonCredit : Reference
    {
        [JsonProperty("role")]
        public string? Role { get; set; }
    }
}
