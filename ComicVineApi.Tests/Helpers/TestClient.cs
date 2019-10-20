using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Clients;
using ComicVineApi.Http;

namespace ComicVineApi.Tests
{
    public class TestClient : ClientBase
    {
        public TestClient(IApiConnection connection)
            : base(connection, 1234, "tests", "test")
        {
        }

        public Filter<TestModel, ITestModelSortable, ITestModelFilterable> Filter() =>
            new Filter<TestModel, ITestModelSortable, ITestModelFilterable>(this);

        public Filter<BiggerTestModel, ITestModelSortable, ITestModelFilterable> BiggerFilter() =>
            new Filter<BiggerTestModel, ITestModelSortable, ITestModelFilterable>(this);

        public Task<IReadOnlyList<TestModel>> FilterAsync(FilterOptions options) =>
            FilterAsync<TestModel>(options);
    }
}
