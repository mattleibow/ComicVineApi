using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Issue : IssueReference, IIssueSortable, IIssueFilterable
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public IReadOnlyList<string>? Aliases { get; set; }

        [JsonProperty("cover_date")]
        public DateTimeOffset? CoverDate { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset? DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset? DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string? Deck { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("has_staff_review")]
        public bool? HasStaffReview { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }

        [JsonProperty("store_date")]
        public DateTimeOffset? StoreDate { get; set; }

        [JsonProperty("volume")]
        public VolumeReference? Volume { get; set; }
    }
}
