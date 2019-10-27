using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class VolumeClient : ClientBase
    {
        public VolumeClient(IApiConnection connection)
            : base(connection, 4050, "volumes", "volume")
        {
        }

        public Filter<Volume, IVolumeSortable, IVolumeFilterable> Filter() =>
            new Filter<Volume, IVolumeSortable, IVolumeFilterable>(this);

        public Task<IReadOnlyList<Volume>> FilterAsync(FilterOptions options) =>
            FilterAsync<Volume>(options);

        public Task<VolumeDetailed> GetAsync(int id) =>
            GetAsync<VolumeDetailed>(id);

        public Task<int> CountAsync() =>
            CountAsync<Volume>();
    }
}
