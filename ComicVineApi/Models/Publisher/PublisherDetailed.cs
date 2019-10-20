using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PublisherDetailed : Publisher
    {
        [JsonProperty("characters")]
        public IReadOnlyList<CharacterReference>? Characters { get; set; }

        [JsonProperty("story_arcs")]
        public IReadOnlyList<StoryArcReference>? StoryArcs { get; set; }

        [JsonProperty("teams")]
        public IReadOnlyList<TeamReference>? Teams { get; set; }

        [JsonProperty("volumes")]
        public IReadOnlyList<VolumeReference>? Volumes { get; set; }
    }
}
