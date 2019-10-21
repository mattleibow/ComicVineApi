using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Series : ComicVineObject, ISeriesSortable, ISeriesFilterable
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public IReadOnlyList<string>? Aliases { get; set; }

        [JsonProperty("count_of_episodes")]
        public int? CountOfEpisodes { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset? DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset? DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string? Deck { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("first_episode")]
        public EpisodeReference? FirstEpisode { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }

        [JsonProperty("last_episode")]
        public EpisodeReference? LastEpisode { get; set; }

        [JsonProperty("publisher")]
        public PublisherReference? Publisher { get; set; }

        [JsonProperty("start_year")]
        public string? StartYear { get; set; }
    }
}
