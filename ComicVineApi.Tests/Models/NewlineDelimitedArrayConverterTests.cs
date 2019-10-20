using ComicVineApi.Models;
using Newtonsoft.Json;
using Xunit;

namespace ComicVineApi.Tests.Models
{
    public class NewlineDelimitedArrayConverterTests
    {
        [Theory]
        [InlineData("null", null)]
        [InlineData("''", new string[0])]
        [InlineData("'first'", new[] { "first" })]
        [InlineData("'first\\nsecond\\nthird'", new[] { "first", "second", "third" })]
        [InlineData("'first\\r\\nsecond\\r\\nthird'", new[] { "first", "second", "third" })]
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
            public string[]? Array { get; set; }
        }
    }
}
