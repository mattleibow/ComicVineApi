using System;
using System.Threading.Tasks;

namespace ComicVineApi.Http
{
    public interface IHttpMessenger
    {
        Task<string> GetAsync(Uri uri);
    }
}
