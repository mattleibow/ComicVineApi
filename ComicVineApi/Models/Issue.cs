using System;
using ComicVineApi.Helpers;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public partial class Issue
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public string[] Aliases { get; set; }

        [JsonProperty("api_detail_url")]
        public Uri ApiDetailUrl { get; set; }

        [JsonProperty("cover_date")]
        public DateTimeOffset? CoverDate { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string Deck { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("has_staff_review")]
        public bool HasStaffReview { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("issue_number")]
        public long IssueNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("site_detail_url")]
        public Uri SiteDetailUrl { get; set; }

        [JsonProperty("store_date")]
        public DateTimeOffset? StoreDate { get; set; }

        [JsonProperty("volume")]
        public Reference Volume { get; set; }
    }
}
