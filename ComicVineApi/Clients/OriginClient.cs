using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class OriginClient : ClientBase
    {
        public OriginClient(IApiConnection connection)
            : base(connection, 4030, "origins", "origin")
        {
        }

        public Filter<Origin, IOriginSortable, IOriginFilterable> Filter() =>
            new Filter<Origin, IOriginSortable, IOriginFilterable>(this);

        public Task<IReadOnlyList<Origin>> FilterAsync(FilterOptions options) =>
            FilterAsync<Origin>(options);

        public Task<OriginDetailed> GetAsync(int id) =>
            GetAsync<OriginDetailed>(id);

        public Task<int> CountAsync() =>
            CountAsync<Origin>();
    }
}
