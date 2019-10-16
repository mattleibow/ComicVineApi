using System.Threading.Tasks;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class IssueClientTests
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
                var characters = await client.Issue.FilterAsync();

                // assert
                Assert.Equal(10, characters.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(6, "The Lost Race", 13)]
            [InlineData(10, null, 73)]
            public async Task ReturnsCorrectData(int id, string name, int number)
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey);
                client.ThrowExceptionOnMissingField = true;

                // act
                var character = await client.Issue.GetAsync(id);

                // assert
                Assert.Equal(name, character.Name);
                Assert.Equal(number, character.IssueNumber);
            }
        }
    }
}
