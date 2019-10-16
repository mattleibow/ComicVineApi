using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterDetailed : Character
    {
        [JsonProperty("character_enemies")]
        public Reference[] CharacterEnemies { get; set; }

        [JsonProperty("character_friends")]
        public Reference[] CharacterFriends { get; set; }

        [JsonProperty("creators")]
        public Reference[] Creators { get; set; }

        [JsonProperty("issue_credits")]
        public Reference[] IssueCredits { get; set; }

        [JsonProperty("issues_died_in")]
        public Reference[] IssuesDiedIn { get; set; }

        [JsonProperty("movies")]
        public Reference[] Movies { get; set; }

        [JsonProperty("powers")]
        public Reference[] Powers { get; set; }

        [JsonProperty("story_arc_credits")]
        public Reference[] StoryArcCredits { get; set; }

        [JsonProperty("team_enemies")]
        public Reference[] TeamEnemies { get; set; }

        [JsonProperty("team_friends")]
        public Reference[] TeamFriends { get; set; }

        [JsonProperty("teams")]
        public Reference[] Teams { get; set; }

        [JsonProperty("volume_credits")]
        public Reference[] VolumeCredits { get; set; }
    }
}
