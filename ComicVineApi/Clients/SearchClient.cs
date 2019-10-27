using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class SearchClient
    {
        private const string resource = "search";

        public SearchClient(IApiConnection connection)
        {
            ApiConnection = connection;
        }

        public IApiConnection ApiConnection { get; }

        public Search Search(string query) =>
            new Search(this, query);

        public async Task<IReadOnlyList<ComicVineObject>> SearchAsync(SearchOptions options)
        {
            var uri = new Uri(resource, UriKind.Relative);
            var result = await ApiConnection.SearchAsync(uri, options).ConfigureAwait(false);
            return result.Results;
        }

        public async Task<int> CountAsync(SearchOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            options = new SearchOptions(options);
            options.FieldList.Clear();
            options.FieldList.Add("id");
            options.Limit = 1;
            options.Offset = 0;

            var uri = new Uri(resource, UriKind.Relative);
            var result = await ApiConnection.SearchAsync(uri, options).ConfigureAwait(false);
            return result.NumberOfTotalResults;
        }
    }
}
