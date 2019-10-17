using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterDetailed : Character
    {
        [JsonProperty("character_enemies")]
        public CharacterReference[] CharacterEnemies { get; set; }

        [JsonProperty("character_friends")]
        public CharacterReference[] CharacterFriends { get; set; }

        [JsonProperty("creators")]
        public CreatorReference[] Creators { get; set; }

        [JsonProperty("issue_credits")]
        public IssueCreditReference[] IssueCredits { get; set; }

        [JsonProperty("issues_died_in")]
        public IssueReference[] IssuesDiedIn { get; set; }

        [JsonProperty("movies")]
        public MovieReference[] Movies { get; set; }

        [JsonProperty("powers")]
        public PowerReference[] Powers { get; set; }

        [JsonProperty("story_arc_credits")]
        public StoryArcReference[] StoryArcCredits { get; set; }

        [JsonProperty("team_enemies")]
        public TeamReference[] TeamEnemies { get; set; }

        [JsonProperty("team_friends")]
        public TeamReference[] TeamFriends { get; set; }

        [JsonProperty("teams")]
        public TeamReference[] Teams { get; set; }

        [JsonProperty("volume_credits")]
        public VolumeReference[] VolumeCredits { get; set; }
    }
}
