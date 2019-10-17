using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class IssueDetailed : Issue
    {
        [JsonProperty("character_credits")]
        public CharacterReference[] CharacterCredits { get; set; }

        [JsonProperty("character_died_in")]
        public IssueReference[] CharacterDiedIn { get; set; }

        [JsonProperty("concept_credits")]
        public ConceptReference[] ConceptCredits { get; set; }

        [JsonProperty("first_appearance_characters")]
        public CharacterReference[] FirstAppearanceCharacters { get; set; }

        [JsonProperty("first_appearance_concepts")]
        public ConceptReference[] FirstAppearanceConcepts { get; set; }

        [JsonProperty("first_appearance_locations")]
        public LocationReference[] FirstAppearanceLocations { get; set; }

        [JsonProperty("first_appearance_objects")]
        public ObjectReference[] FirstAppearanceObjects { get; set; }

        [JsonProperty("first_appearance_storyarcs")]
        public StoryArcReference[] FirstAppearanceStoryarcs { get; set; }

        [JsonProperty("first_appearance_teams")]
        public TeamReference[] FirstAppearanceTeams { get; set; }

        [JsonProperty("location_credits")]
        public LocationReference[] LocationCredits { get; set; }

        [JsonProperty("object_credits")]
        public ObjectCredit[] ObjectCredits { get; set; }

        [JsonProperty("person_credits")]
        public PersonCredit[] PersonCredits { get; set; }

        [JsonProperty("story_arc_credits")]
        public StoryArcReference[] StoryArcCredits { get; set; }

        [JsonProperty("team_credits")]
        public TeamReference[] TeamCredits { get; set; }

        [JsonProperty("team_disbanded_in")]
        public TeamReference[] TeamDisbandedIn { get; set; }
    }
}
