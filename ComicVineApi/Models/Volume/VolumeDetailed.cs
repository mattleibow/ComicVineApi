using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class VolumeDetailed : Volume
    {
        [JsonProperty("characters")]
        public IReadOnlyList<CharacterReference>? Characters { get; set; }

        [JsonProperty("concepts")]
        public IReadOnlyList<ConceptReference>? Concepts { get; set; }

        [JsonProperty("issues")]
        public IReadOnlyList<IssueReference>? Issues { get; set; }

        [JsonProperty("objects")]
        public IReadOnlyList<ObjectReference>? Objects { get; set; }

        [JsonProperty("people")]
        public IReadOnlyList<PersonReference>? People { get; set; }

        [JsonProperty("locations")]
        public IReadOnlyList<LocationReference>? Locations { get; set; }
    }
}
