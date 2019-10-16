using ComicVineApi.Models;
using Newtonsoft.Json;
using Xunit;

namespace ComicVineApi.Tests.Clients
{
    public class CharacterTests
    {
        [Theory]
        [InlineData(0, Gender.Other)]
        [InlineData(1, Gender.Male)]
        [InlineData(2, Gender.Female)]
        public void GenderIsDeserialisedCorrectly(int value, Gender gender)
        {
            // arrange
            var json = $"{{ gender: {value} }}";

            // act
            var obj = JsonConvert.DeserializeObject<Character>(json);

            // assert
            Assert.Equal(gender, obj.Gender);
        }
    }
}
