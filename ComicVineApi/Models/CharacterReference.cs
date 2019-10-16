using System;
using Newtonsoft.Json;

namespace ComicVineApi.Models
{
    public class CharacterReference : Reference
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}
