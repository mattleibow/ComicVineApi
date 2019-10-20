using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SingleResult<T> : ResultBase
    {
        [JsonProperty("results")]
        [JsonConverter(typeof(SometimesArrayConverter))]
        public T Results { get; set; }
    }
}
