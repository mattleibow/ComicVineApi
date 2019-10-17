using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class SeriesClient : ClientBase
    {
        public SeriesClient(IApiConnection connection)
            : base(connection, 4075, "series_list", "series")
        {
        }

        public Filter<Series, ISeriesSortable, ISeriesFilterable> Filter() =>
            new Filter<Series, ISeriesSortable, ISeriesFilterable>(this);

        public Task<IReadOnlyList<Series>> FilterAsync(FilterOptions options) =>
            FilterAsync<Series>(options);

        public Task<SeriesDetailed> GetAsync(long id) =>
            GetAsync<SeriesDetailed>(id);
    }
}
