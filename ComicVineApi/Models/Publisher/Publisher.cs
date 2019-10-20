using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Publisher : PublisherReference
    {
        [JsonProperty("resource_type")]
        internal string? resourceType;

        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public IReadOnlyList<string>? Aliases { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset? DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset? DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string? Deck { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }

        [JsonProperty("location_address")]
        public string? LocationAddress { get; set; }

        [JsonProperty("location_city")]
        public string? LocationCity { get; set; }

        [JsonProperty("location_state")]
        public string? LocationState { get; set; }
    }
}
