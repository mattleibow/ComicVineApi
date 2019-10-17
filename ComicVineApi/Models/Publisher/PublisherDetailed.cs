using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class PublisherDetailed : Publisher
    {
        [JsonProperty("characters")]
        public CharacterReference[] Characters { get; set; }

        [JsonProperty("story_arcs")]
        public StoryArcReference[] StoryArcs { get; set; }

        [JsonProperty("teams")]
        public TeamReference[] Teams { get; set; }

        [JsonProperty("volumes")]
        public VolumeReference[] Volumes { get; set; }
    }
}
