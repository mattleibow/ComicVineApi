using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class IssueDetailed : Issue
    {
        [JsonProperty("character_credits")]
        public IReadOnlyList<CharacterReference>? CharacterCredits { get; set; }

        [JsonProperty("character_died_in")]
        public IReadOnlyList<IssueReference>? CharacterDiedIn { get; set; }

        [JsonProperty("concept_credits")]
        public IReadOnlyList<ConceptReference>? ConceptCredits { get; set; }

        [JsonProperty("first_appearance_characters")]
        public IReadOnlyList<CharacterReference>? FirstAppearanceCharacters { get; set; }

        [JsonProperty("first_appearance_concepts")]
        public IReadOnlyList<ConceptReference>? FirstAppearanceConcepts { get; set; }

        [JsonProperty("first_appearance_locations")]
        public IReadOnlyList<LocationReference>? FirstAppearanceLocations { get; set; }

        [JsonProperty("first_appearance_objects")]
        public IReadOnlyList<ObjectReference>? FirstAppearanceObjects { get; set; }

        [JsonProperty("first_appearance_storyarcs")]
        public IReadOnlyList<StoryArcReference>? FirstAppearanceStoryarcs { get; set; }

        [JsonProperty("first_appearance_teams")]
        public IReadOnlyList<TeamReference>? FirstAppearanceTeams { get; set; }

        [JsonProperty("location_credits")]
        public IReadOnlyList<LocationReference>? LocationCredits { get; set; }

        [JsonProperty("object_credits")]
        public IReadOnlyList<ObjectCredit>? ObjectCredits { get; set; }

        [JsonProperty("person_credits")]
        public IReadOnlyList<PersonCredit>? PersonCredits { get; set; }

        [JsonProperty("story_arc_credits")]
        public IReadOnlyList<StoryArcReference>? StoryArcCredits { get; set; }

        [JsonProperty("team_credits")]
        public IReadOnlyList<TeamReference>? TeamCredits { get; set; }

        [JsonProperty("team_disbanded_in")]
        public IReadOnlyList<TeamReference>? TeamDisbandedIn { get; set; }
    }
}
