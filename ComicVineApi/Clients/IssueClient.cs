using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class IssueClient : ClientBase
    {
        public IssueClient(IApiConnection connection)
            : base(connection, 4000, "issues", "issue")
        {
        }

        public Task<IReadOnlyList<Issue>> FilterAsync(int page = DefaultPageNumber, int count = DefaultPageSize) =>
            FilterAsync(new FilterOptions { PageNumber = page, PageSize = count });

        public Task<IReadOnlyList<Issue>> FilterAsync(FilterOptions options) =>
            FilterAsync<Issue>(options);

        public Task<IssueDetailed> GetAsync(int id) =>
            GetAsync<IssueDetailed>(id);
    }
}
