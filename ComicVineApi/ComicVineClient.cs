using ComicVineApi.Clients;
using ComicVineApi.Http;

namespace ComicVineApi
{
    public class ComicVineClient
    {
        private readonly IHttpConnection connection;

        public ComicVineClient(string apiKey, string? userAgent = null)
        {
            connection = new HttpConnection(apiKey, userAgent);

            var apiConnection = new ApiConnection(connection);
            Character = new CharacterClient(apiConnection);
            Series = new SeriesClient(apiConnection);
            Issue = new IssueClient(apiConnection);
        }

        public CharacterClient Character { get; }

        public SeriesClient Series { get; }

        public IssueClient Issue { get; }

        internal bool ThrowExceptionOnMissingFields
        {
            get => connection.ThrowExceptionOnMissingFields;
            set => connection.ThrowExceptionOnMissingFields = value;
        }
    }
}
