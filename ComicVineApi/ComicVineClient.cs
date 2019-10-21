using System;
using ComicVineApi.Clients;
using ComicVineApi.Http;

namespace ComicVineApi
{
    public class ComicVineClient : IDisposable
    {
        public ComicVineClient(string apiKey, string? userAgent = null)
        {
            HttpMessenger = new HttpMessenger(userAgent);
            HttpConnection = new HttpConnection(HttpMessenger, apiKey);
            ApiConnection = new ApiConnection(HttpConnection);

            Character = new CharacterClient(ApiConnection);
            Series = new SeriesClient(ApiConnection);
            Issue = new IssueClient(ApiConnection);
            Volume = new VolumeClient(ApiConnection);
        }

        public CharacterClient Character { get; }

        public SeriesClient Series { get; }

        public IssueClient Issue { get; }

        public VolumeClient Volume { get; }

        public SearchClient Search { get; }

        internal HttpMessenger HttpMessenger { get; }

        internal IHttpConnection HttpConnection { get; }

        internal ApiConnection ApiConnection { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                HttpMessenger.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
