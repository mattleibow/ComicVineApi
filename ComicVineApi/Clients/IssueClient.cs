using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class IssueClient : ClientBase
    {
        public IssueClient(IApiConnection connection)
            : base(connection, 4000, "issues", "issue")
        {
        }

        public Filter<Issue, IIssueSortable, IIssueFilterable> Filter() =>
            new Filter<Issue, IIssueSortable, IIssueFilterable>(this);

        public Task<IReadOnlyList<Issue>> FilterAsync(FilterOptions options) =>
            FilterAsync<Issue>(options);

        public Task<IssueDetailed> GetAsync(int id) =>
            GetAsync<IssueDetailed>(id);
    }
}
