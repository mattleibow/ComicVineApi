using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Newtonsoft.Json;

namespace ComicVineApi.Http
{
    public class HttpConnection : IHttpConnection
    {
        private static readonly Uri DefaultHost = new Uri("https://comicvine.com/api/");

        private readonly IHttpMessenger httpClient;

        public HttpConnection(IHttpMessenger messenger, string apiKey)
        {
            httpClient = messenger;
            ApiKey = apiKey;
        }

        public string ApiKey { get; }

        public bool ThrowOnMissingProperties { get; set; }

        public async Task<CollectionResult<T>> FilterAsync<T>(Uri uri, Dictionary<string, object>? options)
        {
            uri = GenerateUri(uri, options);

            var json = await httpClient.GetAsync(uri).ConfigureAwait(false);

            var settings = GetSerializerSettings();
            var collection = JsonConvert.DeserializeObject<CollectionResult<T>>(json, settings);

            collection.EnsureSuccessStatusCode();

            return collection;
        }

        public async Task<SingleResult<T>> GetAsync<T>(Uri uri)
        {
            uri = GenerateUri(uri);

            var json = await httpClient.GetAsync(uri).ConfigureAwait(false);

            var settings = GetSerializerSettings();
            var single = JsonConvert.DeserializeObject<SingleResult<T>>(json, settings);

            single.EnsureSuccessStatusCode();

            return single;
        }

        public async Task<SearchResult> SearchAsync(Uri uri, Dictionary<string, object>? options)
        {
            uri = GenerateUri(uri);

            var json = await httpClient.GetAsync(uri).ConfigureAwait(false);

            var settings = GetSerializerSettings();
            var search = JsonConvert.DeserializeObject<SearchResult>(json, settings);

            search.EnsureSuccessStatusCode();

            return search;
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                MissingMemberHandling = ThrowOnMissingProperties
                    ? MissingMemberHandling.Error
                    : MissingMemberHandling.Ignore
            };
        }

        private Uri GenerateUri(Uri uri, Dictionary<string, object>? options = null)
        {
            options = options == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(options);
            options.Add("api_key", ApiKey);
            options.Add("format", "json");

            var builder = new UriBuilder(new Uri(DefaultHost, uri))
            {
                Query = string.Join("&", options.Select(p => $"{p.Key}={Uri.EscapeUriString(p.Value.ToString())}"))
            };

            return builder.Uri;
        }
    }
}
