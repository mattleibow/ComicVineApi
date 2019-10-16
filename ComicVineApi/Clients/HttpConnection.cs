using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Newtonsoft.Json;

namespace ComicVineApi.Clients
{
    public interface IHttpConnection
    {
        string ApiKey { get; }

        bool ThrowExceptionOnMissingField { get; set; }

        Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri);

        Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri, Dictionary<string, object> options);

        Task<T> GetAsync<T>(Uri uri);
    }

    public class HttpConnection : IHttpConnection
    {
        private static readonly Uri DefaultHost = new Uri("https://comicvine.com/api/");

        private readonly HttpClient httpClient;

        public HttpConnection(string apiKey)
        {
            httpClient = new HttpClient();
            ApiKey = apiKey;
        }

        public string ApiKey { get; }

        public bool ThrowExceptionOnMissingField { get; set; }

        public Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri) =>
            FilterAsync<T>(uri, null);

        public async Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri, Dictionary<string, object> options)
        {
            uri = GenerateUri(uri, options);

            var json = await httpClient.GetStringAsync(uri);

            var settings = GetSerializerSettings();
            var collection = JsonConvert.DeserializeObject<CollectionResult<T>>(json, settings);

            return collection.Results;
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            uri = GenerateUri(uri);

            var json = await httpClient.GetStringAsync(uri);

            var settings = GetSerializerSettings();
            var collection = JsonConvert.DeserializeObject<SingleResult<T>>(json, settings);

            return collection.Results;
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                MissingMemberHandling = ThrowExceptionOnMissingField
                    ? MissingMemberHandling.Error
                    : MissingMemberHandling.Ignore
            };
        }

        private Uri GenerateUri(Uri uri, Dictionary<string, object> options = null)
        {
            options = options == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(options);
            options.Add("api_key", ApiKey);
            options.Add("format", "json");

            var builder = new UriBuilder(new Uri(DefaultHost, uri));
            builder.Query = string.Join("&", options.Select(p => $"{p.Key}={Uri.EscapeUriString(p.Value.ToString())}"));

            return builder.Uri;
        }
    }
}
