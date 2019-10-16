using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class SeriesClient : ClientBase
    {
        public SeriesClient(IApiConnection connection)
            : base(connection, 4075, "series_list", "series")
        {
        }

        public Task<IReadOnlyList<Series>> FilterAsync(int page = DefaultPageNumber, int count = DefaultPageSize) =>
            FilterAsync(new FilterOptions { PageNumber = page, PageSize = count });

        public Task<IReadOnlyList<Series>> FilterAsync(FilterOptions options) =>
            FilterAsync<Series>(options);

        public Task<SeriesDetailed> GetAsync(int id) =>
            GetAsync<SeriesDetailed>(id);
    }
}
