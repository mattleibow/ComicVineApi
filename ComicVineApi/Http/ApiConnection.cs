using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Models;

namespace ComicVineApi.Http
{
    public class ApiConnection : IApiConnection
    {
        public ApiConnection(IHttpConnection connection)
        {
            Connection = connection;
        }

        public IHttpConnection Connection { get; }

        public Task<CollectionResult<T>> FilterAsync<T>(Uri uri, FilterOptions? options = null)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));

            var dictionary = new Dictionary<string, object>();
            if (options != null)
            {
                dictionary.Add("offset", options.Offset);
                dictionary.Add("limit", options.Limit);
                if (options.FieldList?.Count > 0)
                    dictionary.Add("field_list", string.Join(",", options.FieldList));
            }

            return Connection.FilterAsync<T>(uri, dictionary);
        }

        public Task<SingleResult<T>> GetAsync<T>(Uri uri)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));

            return Connection.GetAsync<T>(uri);
        }
    }
}
