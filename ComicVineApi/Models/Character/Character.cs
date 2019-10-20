using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Character : CharacterReference, ICharacterSortable, ICharacterFilterable
    {
        [JsonProperty("resource_type")]
        internal string? resourceType;

        [JsonProperty("aliases")]
        [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
        public IReadOnlyList<string>? Aliases { get; set; }

        [JsonProperty("birth")]
        public DateTimeOffset? Birth { get; set; }

        [JsonProperty("count_of_issue_appearances")]
        public int? CountOfIssueAppearances { get; set; }

        [JsonProperty("date_added")]
        public DateTimeOffset? DateAdded { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTimeOffset? DateLastUpdated { get; set; }

        [JsonProperty("deck")]
        public string? Deck { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("first_appeared_in_issue")]
        public IssueReference? FirstAppearedInIssue { get; set; }

        [JsonProperty("gender")]
        public Gender? Gender { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }

        [JsonProperty("origin")]
        public OriginReference? Origin { get; set; }

        [JsonProperty("publisher")]
        public PublisherReference? Publisher { get; set; }

        [JsonProperty("real_name")]
        public string? RealName { get; set; }
    }
}
