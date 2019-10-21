using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class IssueReference : ComicVineObject
    {
        [JsonProperty("issue_number")]
        public string? IssueNumber { get; set; }
    }
}
