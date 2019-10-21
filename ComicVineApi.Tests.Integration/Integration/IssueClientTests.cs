using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Xunit;

namespace ComicVineApi.Tests.Integration
{
    public class IssueClientTests
    {
        public class TheFilterMethod
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var results = await client.Issue.Filter()
                    .Take(3)
                    .ToListAsync();

                // assert
                Assert.Equal(3, results.Count);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(6, "The Lost Race", "13")]
            [InlineData(10, null, "73")]
            public async Task ReturnsCorrectData(int id, string name, string number)
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var result = await client.Issue.GetAsync(id);

                // assert
                Assert.Equal(name, result.Name);
                Assert.Equal(number, result.IssueNumber);
            }
        }

        public class RemoteDataSanitation
        {
            [Fact]
            public async Task ReturnsCorrectData()
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);
                var detailed = new List<IssueDetailed>();

                // act
                var results = await client.Issue.Filter()
                    .Take(3)
                    .ToListAsync();
                foreach (var result in results)
                {
                    var res = await client.Issue.GetAsync(result.Id!.Value);
                    detailed.Add(res);
                }

                // assert
                Assert.Equal(3, detailed.Count);
            }
        }
    }
}
