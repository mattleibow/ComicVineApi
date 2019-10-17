using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Http;
using ComicVineApi.Models;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace ComicVineApi.Tests.Clients
{
    public class FilterTests
    {
        public class TheCountAsyncMethod
        {
            [Fact]
            public async Task OverwritesAllParameters()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .Take(10)
                    .CountAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o =>
                        o.Count == 3 &&
                        o["offset"].Equals(0) &&
                        o["limit"].Equals(1) &&
                        o["field_list"].Equals("id")));
            }
        }

        public class TheFirstOrDefaultAsyncMethod
        {
            [Fact]
            public async Task OverwritesLimitParameter()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .Take(10)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["offset"].Equals(0) && o["limit"].Equals(1)));
            }

            [Fact]
            public async Task PreservesOffsetParameter()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .Skip(10)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["offset"].Equals(10) && o["limit"].Equals(1)));
            }
        }

        public class FilterOptionsTests
        {
            [Fact]
            public async Task DefaultValuesAreCorrect()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var models = await client.Filter()
                    .ToListAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Is<Uri>(u => u.ToString() == "tests"),
                    Arg.Is<Dictionary<string, object>>(o => o.Count == 2 && o["offset"].Equals(0) && o["limit"].Equals(100)));
            }
        }

        public class TheIncludeMethod
        {
            [Fact]
            public async Task NoIncludeFieldDoesNotHaveFieldListParameter()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => !o.ContainsKey("field_list")));
            }

            [Fact]
            public async Task IncludeFieldSpecifiesCorrectParameters()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .IncludeField(c => c.Id)
                    .IncludeField(c => c.FirstName)
                    .IncludeField(c => c.Age)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["field_list"].Equals("id,first_name,age")));
            }

            [Fact]
            public async Task IncludeSupportsSplittingRequests()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<TestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<TestModel> { Results = Array.Empty<TestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var filter = client.Filter()
                    .IncludeField(c => c.Id);
                var filterA = filter
                    .IncludeField(c => c.FirstName);
                var filterB = filter
                    .IncludeField(c => c.Age);

                // assert
                var model = await filter.FirstOrDefaultAsync();
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["field_list"].Equals("id")));

                var modelA = await filterA.FirstOrDefaultAsync();
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["field_list"].Equals("id,first_name")));

                var modelB = await filterB.FirstOrDefaultAsync();
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["field_list"].Equals("id,age")));
            }
        }

        // helper classes

        public class TestClient : ClientBase
        {
            public TestClient(IApiConnection connection)
                : base(connection, 1234, "tests", "test")
            {
            }

            public Filter<TestModel, ITestModelSortable, ITestModelFilterable> Filter() =>
                new Filter<TestModel, ITestModelSortable, ITestModelFilterable>(this);

            public Task<IReadOnlyList<TestModel>> FilterAsync(FilterOptions options) =>
                FilterAsync<TestModel>(options);
        }

        public class TestModel : ITestModelSortable, ITestModelFilterable
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }

            [JsonProperty("age")]
            public int Age { get; set; }

            [JsonProperty("birthday")]
            public DateTimeOffset Birthday { get; set; }
        }

        public interface ITestModelSortable
        {
            int Id { get; set; }

            string LastName { get; set; }

            int Age { get; set; }
        }

        public interface ITestModelFilterable
        {
            int Id { get; set; }

            string FirstName { get; set; }

            string LastName { get; set; }
        }
    }
}
