using System;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Models;

namespace ComicVineApi.Http
{
    public interface IApiConnection
    {
        IHttpConnection Connection { get; }

        Task<CollectionResult<T>> FilterAsync<T>(Uri uri, FilterOptions? options = null);

        Task<SingleResult<T>> GetAsync<T>(Uri uri);
    }
}
