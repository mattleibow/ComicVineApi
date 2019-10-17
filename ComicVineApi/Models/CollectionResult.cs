using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CollectionResult<T> : ResultBase
    {
        [JsonProperty("results")]
        public T[] Results { get; set; }
    }
}
