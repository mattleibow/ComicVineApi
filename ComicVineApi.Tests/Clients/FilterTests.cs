using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Http;
using ComicVineApi.Models;
using NSubstitute;
using Xunit;

namespace ComicVineApi.Tests.Clients
{
    public class FilterTests
    {
        public class FilterOptionsObject
        {
            [Fact]
            public void CloneCopiesEverything()
            {
                // arrange
                var original = new FilterOptions
                {
                    FieldList = { "id", "name" },
                    Limit = 33,
                    Offset = 44,
                    SortDescending = true,
                    SortField = "age",
                    Filter = { { "key", "value" } }
                };

                // act
                var clone = new FilterOptions(original);

                // assert
                Assert.Equal(new[] { "id", "name" }, clone.FieldList);
                Assert.Equal(33, clone.Limit);
                Assert.Equal(44, clone.Offset);
                Assert.True(clone.SortDescending);
                Assert.Equal("age", clone.SortField);
                Assert.Equal("value", clone.Filter["key"]);
            }
        }

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

        public class TheOrderByAndOrderByDescendingMethods
        {
            [Fact]
            public async Task OrderByAddsTheParameter()
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
                    .OrderBy(x => x.LastName)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["sort"].Equals("last_name:asc")));
            }

            [Fact]
            public async Task OrderByDescendingAddsTheParameter()
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
                    .OrderByDescending(x => x.LastName)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["sort"].Equals("last_name:desc")));
            }

            [Fact]
            public async Task OrderByAddsTheParameterFromBaseType()
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                httpConnection
                    .FilterAsync<BiggerTestModel>(Arg.Any<Uri>(), Arg.Any<Dictionary<string, object>>())
                    .Returns(Task.FromResult(new CollectionResult<BiggerTestModel> { Results = Array.Empty<BiggerTestModel>() }));
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.BiggerFilter()
                    .OrderBy(x => x.LastName)
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<BiggerTestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["sort"].Equals("last_name:asc")));
            }
        }

        public class TheWithValueMethod
        {
            [Fact]
            public async Task FilterIsAddedToParameters()
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
                    .WithValue(x => x.FirstName, "Bob")
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["filter"].Equals("first_name:Bob")));
            }

            [Fact]
            public async Task StringWithSpaceIsEscaped()
            {
                const string response = "{'error':'OK','limit':1,'offset':900,'number_of_page_results':0,'number_of_total_results':717,'status_code':1,'results':[],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var model = await client.Filter()
                    .WithValue(x => x.FirstName, "Bob and Sue")
                    .FirstOrDefaultAsync();

                // assert
                _ = httpMessenger.Received().GetAsync(
                    Arg.Is<Uri>(u => u.Query.Contains("filter=first_name:Bob%20and%20Sue", StringComparison.OrdinalIgnoreCase)));
            }

            [Fact]
            public async Task SingleDateFilterUsesDateAndNow()
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
                    .WithValue(x => x.Date, new DateTimeOffset(2010, 03, 06, 0, 0, 0, TimeSpan.Zero))
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["filter"].Equals("date:2010-03-06|9999-12-31")));
            }

            [Fact]
            public async Task DateFilterUsesBothDates()
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
                    .WithValue(
                        x => x.Date,
                        new DateTimeOffset(2010, 03, 06, 0, 0, 0, TimeSpan.Zero),
                        new DateTimeOffset(2020, 04, 08, 0, 0, 0, TimeSpan.Zero))
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["filter"].Equals("date:2010-03-06|2020-04-08")));
            }

            [Fact]
            public async Task NullableDateFilterUsesBothDates()
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
                    .WithValue(
                        x => x.NullableDate,
                        new DateTimeOffset(2010, 03, 06, 0, 0, 0, TimeSpan.Zero),
                        new DateTimeOffset(2020, 04, 08, 0, 0, 0, TimeSpan.Zero))
                    .FirstOrDefaultAsync();

                // assert
                _ = httpConnection.Received().FilterAsync<TestModel>(
                    Arg.Any<Uri>(),
                    Arg.Is<Dictionary<string, object>>(o => o["filter"].Equals("nullable_date:2010-03-06|2020-04-08")));
            }
        }

        public class TheToAsyncEnumerableMethod
        {
            [Theory]
            [InlineData(0, 0)]
            [InlineData(5, 5)]
            [InlineData(10, 10)]
            [InlineData(15, 15)]
            [InlineData(20, 20)]
            [InlineData(25, 20)]
            [InlineData(100, 20)]
            public async Task FetchesTheCorrectAmount(int desiredSize, int actualSize)
            {
                // arrange
                var httpConnection = Substitute.For<IHttpConnection>();
                CreateFilterResultsCalls(httpConnection);
                var apiConnection = new ApiConnection(httpConnection);
                var client = new TestClient(apiConnection);

                // act
                var filter = client.Filter()
                    .Take(desiredSize);
                //.ToAsyncEnumerable();
                var results = new List<int>();
                await foreach (var res in ToAsyncEnumerable(filter))
                    results.Add(res.Id!.Value);

                // assert
                Assert.Equal(Enumerable.Range(1, actualSize), results);
            }

            private static void CreateFilterResultsCalls(IHttpConnection httpConnection)
            {
                httpConnection
                    .FilterAsync<TestModel>(
                        Arg.Any<Uri>(),
                        Arg.Is<Dictionary<string, object>>(o => o["offset"].Equals(0)))
                    .Returns(Task.FromResult(new CollectionResult<TestModel>
                    {
                        Results = Enumerable.Range(1, 10)
                            .Select(i => new TestModel { Id = i })
                            .ToArray()
                    }));
                httpConnection
                    .FilterAsync<TestModel>(
                        Arg.Any<Uri>(),
                        Arg.Is<Dictionary<string, object>>(o => o["offset"].Equals(10)))
                    .Returns(Task.FromResult(new CollectionResult<TestModel>
                    {
                        Results = Enumerable.Range(11, 10)
                            .Select(i => new TestModel { Id = i })
                            .ToArray()
                    }));
                httpConnection
                    .FilterAsync<TestModel>(
                        Arg.Any<Uri>(),
                        Arg.Is<Dictionary<string, object>>(o => (int)o["offset"] > 10))
                    .Returns(Task.FromResult(new CollectionResult<TestModel>
                    {
                        Results = Array.Empty<TestModel>()
                    }));
            }

            public async IAsyncEnumerable<TModel> ToAsyncEnumerable<TModel, TSortable, TFilterable>(Filter<TModel, TSortable, TFilterable> filter)
                where TModel : ComicVineObject, TSortable, TFilterable
            {
                var options = filter.ToOptions();
                var client = filter.client;

                var itemsSoFar = 0;

                // don't request more than we need
                while (itemsSoFar < options.Limit)
                {
                    var results = await client.FilterAsync<TModel>(options);

                    // no more items in the source
                    if (results.Count == 0)
                        yield break;

                    // just return the requested amount
                    var limited = results.Take(options.Limit - itemsSoFar);
                    foreach (var res in limited)
                        yield return res;

                    options.Offset += results.Count;
                    options.Limit -= results.Count;
                    itemsSoFar += results.Count;
                }
            }
        }
    }
}
