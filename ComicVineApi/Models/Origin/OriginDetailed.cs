using System.Collections.Generic;
using Newtonsoft.Json;
namespace ComicVineApi.Models
{
    public class OriginDetailed : Origin
    {
        [JsonProperty("character_set")]
        public object? CharacterSet { get; set; }

        [JsonProperty("characters")]
        public IReadOnlyList<CharacterReference>? Characters { get; set; }

        [JsonProperty("profiles")]
        public IReadOnlyList<object>? Profiles { get; set; }
    }
}
