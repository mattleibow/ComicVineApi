using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace ComicVineApi.Tests.Http
{
    public class HttpConnectionTests
    {
        public class TheFilterAsyncMethod
        {
            [Fact]
            public async Task JsonWithItemsHaveObjectsInTheResult()
            {
                const string response = "{'error':'OK','limit':1,'offset':0,'number_of_page_results':1,'number_of_total_results':717,'status_code':1,'results':[{'id':1}],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act
                var result = await httpConnection.FilterAsync<TestModel>(new Uri("anything", UriKind.Relative), null);

                // assert
                Assert.Equal(StatusCode.Ok, result.StatusCode);
                Assert.NotEmpty(result.Results);
            }

            [Fact]
            public async Task JsonWithNoItemsDoNotHaveObjectsInTheResult()
            {
                const string response = "{'error':'OK','limit':1,'offset':900,'number_of_page_results':0,'number_of_total_results':717,'status_code':1,'results':[],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act
                var result = await httpConnection.FilterAsync<TestModel>(new Uri("anything", UriKind.Relative), null);

                // assert
                Assert.Equal(StatusCode.Ok, result.StatusCode);
                Assert.Empty(result.Results);
            }

            [Fact]
            public async Task ParametersWithSpacesAreEscaped()
            {
                const string response = "{'error':'OK','limit':1,'offset':900,'number_of_page_results':0,'number_of_total_results':717,'status_code':1,'results':[],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);
                var options = new Dictionary<string, object>
                {
                    { "key", "option:value with space" }
                };

                // act
                var result = await httpConnection.FilterAsync<TestModel>(new Uri("anything", UriKind.Relative), options);

                // assert
                _ = httpMessenger.Received().GetAsync(
                    Arg.Is<Uri>(u => u.Query.Contains("key=option:value%20with%20space", StringComparison.OrdinalIgnoreCase)));
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task JsonWithItemHasObjecInTheResult()
            {
                const string response = "{'error':'OK','limit':1,'offset':0,'number_of_page_results':1,'number_of_total_results':1,'status_code':1,'results':{'id':1253},'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act
                var result = await httpConnection.GetAsync<TestModel>(new Uri("anything", UriKind.Relative));

                // assert
                Assert.Equal(StatusCode.Ok, result.StatusCode);
                Assert.NotNull(result.Results);
                Assert.Equal(1253, result.Results.Id);
            }

            [Fact]
            public async Task JsonWithItemArrayDoesNotThrow()
            {
                const string response = "{'error':'OK','limit':1,'offset':0,'number_of_page_results':1,'number_of_total_results':1,'status_code':1,'results':[],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act
                var result = await httpConnection.GetAsync<TestModel>(new Uri("anything", UriKind.Relative));

                // assert
                Assert.Equal(StatusCode.Ok, result.StatusCode);
                Assert.Null(result.Results);
            }

            [Fact]
            public async Task ThrownHttpExceptionPropagates()
            {
                // arrange
                var exception = new HttpRequestException("This is an expected exception.");
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns<Task<string>>(x => throw exception);
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act & assert
                await Assert.ThrowsAsync<HttpRequestException>(() => httpConnection.GetAsync<TestModel>(new Uri("anything", UriKind.Relative)));
            }

            [Fact]
            public async Task ErrorStatusCodeResultsInException()
            {
                const string response = "{'error':'Object Not Found','limit':0,'offset':0,'number_of_page_results':0,'number_of_total_results':0,'status_code':101,'results':[]}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act & assert
                var exception = await Assert.ThrowsAsync<ComicVineException>(() => httpConnection.GetAsync<TestModel>(new Uri("anything", UriKind.Relative)));
                Assert.Contains("Object Not Found", exception.Message, StringComparison.OrdinalIgnoreCase);
            }
        }

        public class TheSearchAsyncMethod
        {
            [Fact]
            public async Task CanDeserializeHeterogenousResults()
            {
                const string response = "{'error':'OK','limit':10,'offset':0,'number_of_page_results':10,'number_of_total_results':11554,'status_code':1,'results':[{'id':1699,'name':'Batman','resource_type':'character'},{'id':154468,'name':'Batman','resource_type':'character'},{'id':131296,'name':'Bat - Man','resource_type':'character'},{'id':45117,'name':'Bat - Man','resource_type':'character'},{'id':59060,'name':'Bat - Man','resource_type':'character'},{'id':31,'name':'Batman','resource_type':'series'},{'id':796,'name':'Batman','resource_type':'volume'},{'id':38,'name':'The Batman','resource_type':'series'},{'id':77146,'name':'Batman','resource_type':'volume'},{'id':41807,'name':'Batman','resource_type':'volume'}],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);

                // act
                var result = await httpConnection.SearchAsync(new Uri("anything", UriKind.Relative), null);

                // assert
                Assert.Equal(StatusCode.Ok, result.StatusCode);
                Assert.NotEmpty(result.Results);
                Assert.Equal(10, result.Results.Count);
            }
        }

        public class TheThrowOnMissingPropertiesProperty
        {
            [Fact]
            public async Task ThrowsWithMissingMember()
            {
                const string response = "{'error':'OK','limit':1,'offset':0,'number_of_page_results':1,'number_of_total_results':717,'status_code':1,'results':[{'id':1,rabbits:true}],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);
                httpConnection.ThrowOnMissingProperties = true;

                // act & assert
                var ex = await Assert.ThrowsAsync<JsonSerializationException>(() => httpConnection.FilterAsync<TestModel>(new Uri("anything", UriKind.Relative), null));
                Assert.Contains("rabbits", ex.Message, StringComparison.OrdinalIgnoreCase);
            }

            [Fact]
            public async Task ThrowsWithMissingResourceType()
            {
                const string response = "{'error':'OK','limit':10,'offset':0,'number_of_page_results':10,'number_of_total_results':11554,'status_code':1,'results':[{'id':1,'resource_type':'character'},{'id':2,'resource_type':'rabbits'}],'version':'1.0'}";

                // arrange
                var httpMessenger = Substitute.For<IHttpMessenger>();
                httpMessenger.GetAsync(Arg.Any<Uri>()).Returns(Task.FromResult(response));
                var httpConnection = new HttpConnection(httpMessenger, Settings.ApiKey);
                httpConnection.ThrowOnMissingProperties = true;

                // act & assert
                var ex = await Assert.ThrowsAsync<JsonSerializationException>(() => httpConnection.SearchAsync(new Uri("anything", UriKind.Relative), null));
                Assert.Contains("rabbits", ex.Message, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
