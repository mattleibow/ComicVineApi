using System;
using ComicVineApi.Helpers;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Series
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public string[] Aliases { get; set; }

        [JsonProperty("api_detail_url")]
        public Uri ApiDetailUrl { get; set; }

        [JsonProperty("count_of_episodes")]
        public long CountOfEpisodes { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string Deck { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("first_episode")]
        public EpisodeReference FirstEpisode { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("last_episode")]
        public EpisodeReference LastEpisode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publisher")]
        public Reference Publisher { get; set; }

        [JsonProperty("site_detail_url")]
        public Uri SiteDetailUrl { get; set; }

        [JsonProperty("start_year")]
        public string StartYear { get; set; }
    }
}
