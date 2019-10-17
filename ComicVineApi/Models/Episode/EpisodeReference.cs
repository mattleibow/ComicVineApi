using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class EpisodeReference : Reference
    {
        [JsonProperty("episode_number")]
        public string EpisodeNumber { get; set; }
    }
}
