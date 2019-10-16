using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public partial class PersonCredit : Reference
    {
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
