using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicVineApi.Clients
{
    public interface IApiConnection
    {
        IHttpConnection Connection { get; }

        Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri);

        Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri, FilterOptions options);

        Task<T> GetAsync<T>(Uri uri);
    }

    public class ApiConnection : IApiConnection
    {
        public ApiConnection(IHttpConnection connection)
        {
            Connection = connection;
        }

        public IHttpConnection Connection { get; }

        public Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri) =>
            FilterAsync<T>(uri, null);

        public async Task<IReadOnlyList<T>> FilterAsync<T>(Uri uri, FilterOptions options)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));

            var dictionary = options == null
                ? null
                : new Dictionary<string, object> {
                    { "offset", options.PageNumber },
                    { "limit", options.PageSize }
                };
            var result = await Connection.FilterAsync<T>(uri, dictionary);
            return result;
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));

            var result = await Connection.GetAsync<T>(uri);
            return result;
        }
    }
}
