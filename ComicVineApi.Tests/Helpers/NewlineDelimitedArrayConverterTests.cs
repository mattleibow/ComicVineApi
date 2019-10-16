using ComicVineApi.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace ComicVineApi.Tests.Helpers
{
    public class NewlineDelimitedArrayConverterTests
    {
        [Theory]
        [InlineData("null", null)]
        [InlineData("''", new [] { "" })]
        [InlineData("'first'", new [] { "first" })]
        [InlineData("'first\\nsecond\\nthird'", new [] { "first", "second", "third" })]
        public void ConvertsCorrectly(string value, string[] array)
        {
            // arrange
            var json = $"{{ array: {value} }}";

            // act
            var obj = JsonConvert.DeserializeObject<TestObject>(json);

            // assert
            Assert.Equal(array, obj.Array);
        }

        public class TestObject
        {
            [JsonConverter(typeof(NewlineDelimitedArrayConverter))]
            public string[] Array { get; set; }
        }
    }
}
