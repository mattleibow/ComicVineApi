using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CollectionResult<T> : ResultBase
    {
        [JsonProperty("results")]
        public IReadOnlyList<T> Results { get; set; }
    }
}
