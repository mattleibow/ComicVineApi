using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicVineApi.Clients
{
    public abstract class ClientBase
    {
        protected const int DefaultPageNumber = 0;
        protected const int DefaultPageSize = 100;

        private int endpointId;
        private string filterResource;
        private string getResource;

        protected ClientBase(IApiConnection connection, int endpointId, string filterResource, string getResource)
        {
            this.endpointId = endpointId;
            this.filterResource = filterResource;
            this.getResource = getResource;

            ApiConnection = connection;
        }

        protected IApiConnection ApiConnection { get; }

        public async Task<IReadOnlyList<T>> FilterAsync<T>(FilterOptions options)
        {
            var uri = new Uri(filterResource, UriKind.Relative);
            var result = await ApiConnection.FilterAsync<T>(uri, options);
            return result;
        }

        public async Task<T> GetAsync<T>(int id)
        {
            Uri uri = new Uri($"{getResource}/{endpointId}-{id}", UriKind.Relative);
            var result = await ApiConnection.GetAsync<T>(uri);
            return result;
        }
    }
}
