using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;
using NSubstitute;
using Xunit;

namespace ComicVineApi.Tests.Http
{
    public class ApiConnectionTests
    {
        public class TheFilterAsyncMethod
        {
            [Fact]
            public async Task ThrowsArgumentNullWhenNull()
            {
                // arrange
                var uri = new Uri("anything", UriKind.Relative);
                var httpConnection = Substitute.For<IHttpConnection>();
                var apiConnection = new ApiConnection(httpConnection);

                // assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => apiConnection.FilterAsync<object>(null));
            }

            [Fact]
            public async Task MakesCorrectRequest()
            {
                // arrange
                var uri = new Uri("anything", UriKind.Relative);
                var result = new CollectionResult<object> { Results = Array.Empty<object>() };
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<object>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(result));
                var apiConnection = new ApiConnection(httpConnection);

                // act
                var characters = await apiConnection.FilterAsync<object>(uri);

                // assert
                Assert.Same(result, characters);
                _ = httpConnection.Received().FilterAsync<object>(
                    Arg.Is<Uri>(u => u.ToString() == "anything"),
                    Arg.Any<Dictionary<string, object>>());
            }
        }
    }
}
