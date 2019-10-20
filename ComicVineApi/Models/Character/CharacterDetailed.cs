using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterDetailed : Character
    {
        [JsonProperty("character_enemies")]
        public IReadOnlyList<CharacterReference>? CharacterEnemies { get; set; }

        [JsonProperty("character_friends")]
        public IReadOnlyList<CharacterReference>? CharacterFriends { get; set; }

        [JsonProperty("creators")]
        public IReadOnlyList<PersonReference>? Creators { get; set; }

        [JsonProperty("issue_credits")]
        public IReadOnlyList<IssueCreditReference>? IssueCredits { get; set; }

        [JsonProperty("issues_died_in")]
        public IReadOnlyList<IssueReference>? IssuesDiedIn { get; set; }

        [JsonProperty("movies")]
        public IReadOnlyList<MovieReference>? Movies { get; set; }

        [JsonProperty("powers")]
        public IReadOnlyList<PowerReference>? Powers { get; set; }

        [JsonProperty("story_arc_credits")]
        public IReadOnlyList<StoryArcReference>? StoryArcCredits { get; set; }

        [JsonProperty("team_enemies")]
        public IReadOnlyList<TeamReference>? TeamEnemies { get; set; }

        [JsonProperty("team_friends")]
        public IReadOnlyList<TeamReference>? TeamFriends { get; set; }

        [JsonProperty("teams")]
        public IReadOnlyList<TeamReference>? Teams { get; set; }

        [JsonProperty("volume_credits")]
        public IReadOnlyList<VolumeReference>? VolumeCredits { get; set; }
    }
}
