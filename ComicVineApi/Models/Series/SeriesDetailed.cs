using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SeriesDetailed : Series
    {
        [JsonProperty("characters")]
        public SeriesCharacterReference[] Characters { get; set; }

        [JsonProperty("episodes")]
        public EpisodeReference[] Episodes { get; set; }
    }
}
