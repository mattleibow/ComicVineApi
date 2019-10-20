using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class Reference
    {
        [JsonProperty("api_detail_url")]
        public Uri? ApiDetailUrl { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("site_detail_url")]
        public Uri? SiteDetailUrl { get; set; }
    }
}
