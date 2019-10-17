using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Newtonsoft.Json;

namespace ComicVineApi.Http
{
    public class HttpConnection : IHttpConnection, IDisposable
    {
        private static readonly Uri DefaultHost = new Uri("https://comicvine.com/api/");

        private readonly HttpClient httpClient;

        public HttpConnection(string apiKey, string? userAgent = null)
        {
            httpClient = new HttpClient();

            if (!string.IsNullOrWhiteSpace(userAgent))
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            UserAgent = userAgent;
            ApiKey = apiKey;
        }

        public string? UserAgent { get; }

        public string ApiKey { get; }

        public bool ThrowExceptionOnMissingFields { get; set; }

        public async Task<CollectionResult<T>> FilterAsync<T>(Uri uri, Dictionary<string, object>? options)
        {
            uri = GenerateUri(uri, options);

            var json = await httpClient.GetStringAsync(uri).ConfigureAwait(false);

            var settings = GetSerializerSettings();
            var collection = JsonConvert.DeserializeObject<CollectionResult<T>>(json, settings);

            return collection;
        }

        public async Task<SingleResult<T>> GetAsync<T>(Uri uri)
        {
            uri = GenerateUri(uri);

            var json = await httpClient.GetStringAsync(uri).ConfigureAwait(false);

            var settings = GetSerializerSettings();
            var single = JsonConvert.DeserializeObject<SingleResult<T>>(json, settings);

            return single;
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                MissingMemberHandling = ThrowExceptionOnMissingFields
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                httpClient.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
