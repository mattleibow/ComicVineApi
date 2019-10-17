using System;
using ComicVineApi.Helpers;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Publisher : PublisherReference
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public string[] Aliases { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string Deck { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("location_address")]
        public string LocationAddress { get; set; }

        [JsonProperty("location_city")]
        public string LocationCity { get; set; }

        [JsonProperty("location_state")]
        public string LocationState { get; set; }
    }
}
