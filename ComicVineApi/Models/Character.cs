using System;
using ComicVineApi.Helpers;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public interface ICharacterSortable
    {
        DateTimeOffset DateAdded { get; set; }

        DateTimeOffset DateLastUpdated { get; set; }

        long Id { get; set; }

        string Name { get; set; }
    }

    public interface ICharacterFilterable
    {
        DateTimeOffset DateAdded { get; set; }

        DateTimeOffset DateLastUpdated { get; set; }

        Gender Gender { get; set; }

        long Id { get; set; }

        string Name { get; set; }
    }

    public class Character : ICharacterSortable, ICharacterFilterable
    {
        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public string[] Aliases { get; set; }

        [JsonProperty("api_detail_url")]
        public Uri ApiDetailUrl { get; set; }

        [JsonProperty("birth")]
        public DateTimeOffset? Birth { get; set; }

        [JsonProperty("count_of_issue_appearances")]
        public long CountOfIssueAppearances { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string Deck { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("first_appeared_in_issue")]
        public IssueReference FirstAppearedInIssue { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin")]
        public Reference Origin { get; set; }

        [JsonProperty("publisher")]
        public Reference Publisher { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("site_detail_url")]
        public Uri SiteDetailUrl { get; set; }
    }
}
