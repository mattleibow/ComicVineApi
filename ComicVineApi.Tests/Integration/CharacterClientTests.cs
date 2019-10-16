using System.Threading.Tasks;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class CharacterClientTests
    {
        public class TheFilterAsyncMethod
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey);
                client.ThrowExceptionOnMissingField = true;

                // act
                var characters = await client.Character.FilterAsync();

                // assert
                Assert.Equal(10, characters.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(1253, "Lightning Lad")]
            public async Task ReturnsCorrectData(int id, string name)
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey);
                client.ThrowExceptionOnMissingField = true;

                // act
                var character = await client.Character.GetAsync(id);

                // assert
                Assert.Equal(name, character.Name);
            }
        }
    }
}
