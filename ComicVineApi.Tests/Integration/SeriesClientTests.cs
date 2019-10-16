using System.Threading.Tasks;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class SeriesClientTests
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
                var characters = await client.Series.FilterAsync();

                // assert
                Assert.Equal(10, characters.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(1, "Agents of S.H.I.E.L.D.")]
            [InlineData(346, "Pokémon: Advanced")]
            public async Task ReturnsCorrectData(int id, string name)
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey);
                client.ThrowExceptionOnMissingField = true;

                // act
                var character = await client.Series.GetAsync(id);

                // assert
                Assert.Equal(name, character.Name);
            }
        }
    }
}
