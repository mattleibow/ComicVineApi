using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class IssueReference : Reference
    {
        [JsonProperty("issue_number")]
        public long IssueNumber { get; set; }
    }
}
