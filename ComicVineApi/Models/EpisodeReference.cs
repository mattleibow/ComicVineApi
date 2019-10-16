using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class EpisodeReference : Reference
    {
        [JsonProperty("episode_number")]
        public long EpisodeNumber { get; set; }
    }
}
