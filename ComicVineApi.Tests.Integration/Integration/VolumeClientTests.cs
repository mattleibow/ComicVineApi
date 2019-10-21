using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class VolumeClientTests
    {
        public class TheFilterMethod
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var results = await client.Volume.Filter()
                    .Take(3)
                    .ToListAsync();

                // assert
                Assert.Equal(3, results.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(769, "Jumbo Comics")]
            [InlineData(816, "Planet Comics")]
            public async Task ReturnsCorrectData(int id, string name)
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var result = await client.Volume.GetAsync(id);

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
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);
                var detailed = new List<VolumeDetailed>();

                // act
                var results = await client.Volume.Filter()
                    .Take(3)
                    .ToListAsync();
                foreach (var result in results)
                {
                    var res = await client.Volume.GetAsync(result.Id!.Value);
                    detailed.Add(res);
                }

                // assert
                Assert.Equal(3, detailed.Count);
            }
        }
    }
}
