using ComicVineApi.Clients;

namespace ComicVineApi
{
    public class ComicVineClient
    {
        private readonly IHttpConnection connection;

        public ComicVineClient(string apiKey)
        {
            connection = new HttpConnection(apiKey);

            var apiConnection = new ApiConnection(connection);
            Character = new CharacterClient(apiConnection);
            Series = new SeriesClient(apiConnection);
            Issue = new IssueClient(apiConnection);
        }

        public CharacterClient Character { get; }

        public SeriesClient Series { get; }

        public IssueClient Issue { get; }

        public bool ThrowExceptionOnMissingField
        {
            get => connection.ThrowExceptionOnMissingField;
            set => connection.ThrowExceptionOnMissingField = value;
        }
    }
}
