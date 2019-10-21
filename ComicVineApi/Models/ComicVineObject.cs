using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class ComicVineObject
    {
        [JsonProperty("resource_type")]
        internal string? resourceType;

        [JsonProperty("api_detail_url")]
        internal Uri? apiDetailUrl;

        [JsonProperty("site_detail_url")]
        internal Uri? siteDetailUrl;

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
