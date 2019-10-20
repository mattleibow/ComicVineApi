using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComicVineApi.Http
{
    public class HttpMessenger : IHttpMessenger, IDisposable
    {
        private readonly HttpClient httpClient;

        public HttpMessenger(string? userAgent = null)
        {
            httpClient = new HttpClient();

            if (!string.IsNullOrWhiteSpace(userAgent))
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            UserAgent = userAgent;
        }

        public string? UserAgent { get; }

        public Task<string> GetAsync(Uri uri) =>
            httpClient.GetStringAsync(uri);

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
