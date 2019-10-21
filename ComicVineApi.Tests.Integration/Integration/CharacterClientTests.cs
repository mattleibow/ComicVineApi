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
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var results = await client.Character.Filter()
                    .Take(3)
                    .ToListAsync();

                // assert
                Assert.Equal(3, results.Count);
            }

            [Fact]
            public async Task CanFindRedHood()
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

                // act
                var results = await client.Character.Filter()
                    .IncludeField(x => x.Id)
                    .IncludeField(x => x.Name)
                    .IncludeField(x => x.Aliases)
                    .IncludeField(x => x.Deck)
                    .Take(5)
                    .WithValue(x => x.Name, "Jason Todd")
                    .ToListAsync();

                // assert
                Assert.Single(results);
                Assert.Contains("Red Hood", results[0].Aliases);
            }
        }

        public class TheGetAsyncMethod
        {
            [Theory]
            [InlineData(1253, "Lightning Lad")]
            public async Task ReturnsCorrectData(int id, string name)
            {
                // arrange
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);

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
                using var client = new ComicVineClient(Settings.ApiKey, Settings.UserAgent);
                var detailed = new List<CharacterDetailed>();

                // act
                var results = await client.Character.Filter()
                    .Take(3)
                    .ToListAsync();
                foreach (var result in results)
                {
                    var res = await client.Character.GetAsync(result.Id!.Value);
                    detailed.Add(res);
                }

                // assert
                Assert.Equal(3, detailed.Count);
            }
        }
    }
}
