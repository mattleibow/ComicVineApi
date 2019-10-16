using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public partial class IssueDetailed : Issue
    {
        [JsonProperty("character_credits")]
        public Reference[] CharacterCredits { get; set; }

        [JsonProperty("character_died_in")]
        public Reference[] CharacterDiedIn { get; set; }

        [JsonProperty("concept_credits")]
        public Reference[] ConceptCredits { get; set; }

        [JsonProperty("first_appearance_characters")]
        public Reference[] FirstAppearanceCharacters { get; set; }

        [JsonProperty("first_appearance_concepts")]
        public Reference[] FirstAppearanceConcepts { get; set; }

        [JsonProperty("first_appearance_locations")]
        public Reference[] FirstAppearanceLocations { get; set; }

        [JsonProperty("first_appearance_objects")]
        public Reference[] FirstAppearanceObjects { get; set; }

        [JsonProperty("first_appearance_storyarcs")]
        public Reference[] FirstAppearanceStoryarcs { get; set; }

        [JsonProperty("first_appearance_teams")]
        public Reference[] FirstAppearanceTeams { get; set; }

        [JsonProperty("location_credits")]
        public Reference[] LocationCredits { get; set; }

        [JsonProperty("object_credits")]
        public Reference[] ObjectCredits { get; set; }

        [JsonProperty("person_credits")]
        public PersonCredit[] PersonCredits { get; set; }

        [JsonProperty("story_arc_credits")]
        public Reference[] StoryArcCredits { get; set; }

        [JsonProperty("team_credits")]
        public Reference[] TeamCredits { get; set; }

        [JsonProperty("team_disbanded_in")]
        public Reference[] TeamDisbandedIn { get; set; }
    }
}
