using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ResultBase
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("number_of_page_results")]
        public int NumberOfPageResults { get; set; }

        [JsonProperty("number_of_total_results")]
        public int NumberOfTotalResults { get; set; }

        [JsonProperty("status_code")]
        public StatusCode StatusCode { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        public void EnsureSuccessStatusCode()
        {
            if (StatusCode == StatusCode.Ok)
                return;

            throw new ComicVineException($"Operation failed: '{Error}' ({StatusCode}).");
        }
    }
}
