using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Models;
using NSubstitute;
using Xunit;

namespace ComicVineApi.Tests.Clients
{
    public class CharacterClientTests
    {
        public class TheFilterAsyncMethod
        {
            [Fact]
            public async Task ResolvesCorrectParameters()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                var apiConnection = new ApiConnection(httpConnection);
                var client = new CharacterClient(apiConnection);

                // act
                var characters = await client.FilterAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<Character>(
                    Arg.Is<Uri>(u => u.ToString() == "characters"),
                    Arg.Is<Dictionary<string, object>>(o => o.Count == 2 && o["offset"].Equals(0) && o["limit"].Equals(10)));
            }

            [Fact]
            public async Task ResolvesCorrectUri()
            {
                // arrange
                var apiConnection = Substitute.For<IApiConnection>();
                var client = new CharacterClient(apiConnection);

                // act
                var characters = await client.FilterAsync();

                // assert
                _ = apiConnection.Received().FilterAsync<Character>(
                    Arg.Is<Uri>(u => u.ToString() == "characters"),
                    Arg.Any<FilterOptions>());
            }

            [Fact]
            public async Task ResolvesCorrectPagingValues()
            {
                // arrange
                var apiConnection = Substitute.For<IApiConnection>();
                var client = new CharacterClient(apiConnection);

                // act
                var characters = await client.FilterAsync(count: 10);

                // assert
                _ = apiConnection.Received().FilterAsync<Character>(
                    Arg.Is<Uri>(u => u.ToString() == "characters"),
                    Arg.Is<FilterOptions>(o => o.PageNumber == 0 && o.PageSize == 10));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task IncludeSpecifiesCorrectParameters()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                var apiConnection = new ApiConnection(httpConnection);
                var client = new CharacterClient(apiConnection);

                // act
                var filter = client.GetAll()
                    .Include(c => c.Id)
                    .Include(c => c.Name)
                    .Include(c => c.Deck);
                var characters = await filter.FetchAsync(count: 10);

                // assert
                _ = httpConnection.Received().FilterAsync<Character>(
                    Arg.Is<Uri>(u => u.ToString() == "characters"),
                    Arg.Is<Dictionary<string, object>>(o => o.Count == 2 && o["offset"].Equals(0) && o["limit"].Equals(10)));
            }
        }
    }
}
