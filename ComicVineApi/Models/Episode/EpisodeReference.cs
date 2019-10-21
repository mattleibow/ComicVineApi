using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class EpisodeReference : ComicVineObject
    {
        [JsonProperty("episode_number")]
        public string? EpisodeNumber { get; set; }
    }
}
