using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ResultBase
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("number_of_page_results")]
        public long NumberOfPageResults { get; set; }

        [JsonProperty("number_of_total_results")]
        public long NumberOfTotalResults { get; set; }

        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
