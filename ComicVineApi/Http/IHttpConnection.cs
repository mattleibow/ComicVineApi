using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;

namespace ComicVineApi.Http
{
    public interface IHttpConnection
    {
        string ApiKey { get; }

        bool ThrowOnMissingProperties { get; set; }

        Task<CollectionResult<T>> FilterAsync<T>(Uri uri, Dictionary<string, object>? options);

        Task<SingleResult<T>> GetAsync<T>(Uri uri);

        Task<SearchResult> SearchAsync(Uri uri, Dictionary<string, object>? options);
    }
}
