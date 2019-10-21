using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SearchResult : ResultBase
    {
        [JsonProperty("results")]
        [JsonConverter(typeof(HeterogenousArrayConverter))]
        public IReadOnlyList<ComicVineObject> Results { get; set; }
    }
}
