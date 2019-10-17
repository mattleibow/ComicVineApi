using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class CharacterClientTests
    {
        public class TheFilterMethod
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey, Configuration.UserAgent);
                client.ThrowExceptionOnMissingFields = true;

                // act
                var results = await client.Character.Filter()
                    .Take(3)
                    .ToListAsync();

                // assert
                Assert.Equal(3, results.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(1253, "Lightning Lad")]
            public async Task ReturnsCorrectData(int id, string name)
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey, Configuration.UserAgent);
                client.ThrowExceptionOnMissingFields = true;

                // act
                var result = await client.Character.GetAsync(id);

                // assert
                Assert.Equal(name, result.Name);
            }
        }

        public class RemoteDataSanitation
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                var client = new ComicVineClient(Configuration.ApiKey, Configuration.UserAgent);
                client.ThrowExceptionOnMissingFields = true;
                var detailed = new List<CharacterDetailed>();

                // act
                var results = await client.Character.Filter()
                    .Take(3)
                    .ToListAsync();
                foreach (var result in results)
                {
                    var res = await client.Character.GetAsync(result.Id);
                    detailed.Add(res);
                }

                // assert
                Assert.Equal(3, detailed.Count);
            }
        }
    }
}
