using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SingleResult<T> : ResultBase
    {
        [JsonProperty("results")]
        public T Results { get; set; }
    }
}
