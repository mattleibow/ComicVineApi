using ComicVineApi.Models;
using Newtonsoft.Json;
using Xunit;

namespace ComicVineApi.Tests.Models
{
    public class SometimesArrayConverterTests
    {
        [Theory]
        [InlineData("null", null)]
        [InlineData("{ id: 123 }", 123)]
        [InlineData("[]", null)]
        [InlineData("[{ id: 123 }]", 123)]
        public void ConvertsCorrectly(string value, int? expectedValue)
        {
            // arrange
            var json = $"{{ value: {value} }}";

            // act
            var obj = JsonConvert.DeserializeObject<TestObject>(json);

            // assert
            Assert.Equal(expectedValue, obj.Value?.Id);
        }

        public class TestObject
        {
            [JsonConverter(typeof(SometimesArrayConverter))]
            public ChildObject? Value { get; set; }
        }

        public class ChildObject
        {
            public int Id { get; set; }
        }
    }
}
