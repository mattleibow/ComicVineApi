using System;
using System.Collections.Generic;
using System.Linq;
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
                if (!string.IsNullOrWhiteSpace(options.SortField))
                {
                    var dir = options.SortDescending ? "desc" : "asc";
                    dictionary.Add("sort", $"{options.SortField}:{dir}");
                }
                if (options.Filter?.Count > 0)
                {
                    var filters = options.Filter.Select(x => $"{x.Key}:{x.Value}");
                    dictionary.Add("filter", string.Join(",", filters));
                }
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
