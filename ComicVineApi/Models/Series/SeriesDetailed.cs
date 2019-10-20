using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class SeriesDetailed : Series
    {
        [JsonProperty("characters")]
        public IReadOnlyList<CharacterReference>? Characters { get; set; }

        [JsonProperty("episodes")]
        public IReadOnlyList<EpisodeReference>? Episodes { get; set; }
    }
}
