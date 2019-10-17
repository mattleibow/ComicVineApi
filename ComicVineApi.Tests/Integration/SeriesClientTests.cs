using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class SeriesClientTests
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
                var results = await client.Series.Filter()
                    .Take(3)
                    .ToListAsync();

                // assert
                Assert.Equal(3, results.Count);
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
                var client = new ComicVineClient(Configuration.ApiKey, Configuration.UserAgent);
                client.ThrowExceptionOnMissingFields = true;

                // act
                var result = await client.Series.GetAsync(id);

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
                var detailed = new List<SeriesDetailed>();

                // act
                var results = await client.Series.Filter()
                    .Take(3)
                    .ToListAsync();
                foreach (var result in results)
                {
                    var res = await client.Series.GetAsync(result.Id);
                    detailed.Add(res);
                }

                // assert
                Assert.Equal(3, detailed.Count);
            }
        }
    }
}
