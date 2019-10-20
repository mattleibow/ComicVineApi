using System;
using Newtonsoft.Json;

namespace ComicVineApi.Tests
{
    public class TestModel : ITestModelSortable, ITestModelFilterable
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("birthday")]
        public DateTimeOffset? Birthday { get; set; }

        [JsonProperty("nullable_date")]
        public DateTimeOffset? NullableDate { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }

    public class BiggerTestModel : TestModel
    {
    }
}
