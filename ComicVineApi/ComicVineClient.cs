using System;
using ComicVineApi.Clients;
using ComicVineApi.Http;

namespace ComicVineApi
{
    public class ComicVineClient : IDisposable
    {
        private readonly HttpMessenger httpMessenger;
        private readonly IHttpConnection httpConnection;
        private readonly ApiConnection apiConnection;

        public ComicVineClient(string apiKey, string? userAgent = null)
        {
            httpMessenger = new HttpMessenger(userAgent);
            httpConnection = new HttpConnection(httpMessenger, apiKey);
            apiConnection = new ApiConnection(httpConnection);

            Character = new CharacterClient(apiConnection);
            Series = new SeriesClient(apiConnection);
            Issue = new IssueClient(apiConnection);
            Volume = new VolumeClient(apiConnection);
        }

        public CharacterClient Character { get; }

        public SeriesClient Series { get; }

        public IssueClient Issue { get; }

        public VolumeClient Volume { get; }

        public SearchClient Serach { get; }

        internal HttpMessenger HttpMessenger => httpMessenger;

        internal IHttpConnection HttpConnection => httpConnection;

        internal ApiConnection ApiConnection => apiConnection;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                httpMessenger.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
