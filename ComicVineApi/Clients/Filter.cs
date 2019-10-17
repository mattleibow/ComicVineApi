using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComicVineApi.Clients
{
    public class Filter<TModel, TSortable, TFilterable>
        where TModel : TSortable, TFilterable
    {
        private readonly ClientBase client;

        private readonly FilterOptions options;

        internal Filter(ClientBase client, FilterOptions? options = null)
        {
            this.client = client;
            this.options = options ?? new FilterOptions();
        }

        public Filter<TModel, TSortable, TFilterable> IncludeField<TValue>(Expression<Func<TModel, TValue>> property)
        {
            _ = property ?? throw new ArgumentNullException(nameof(property));

            var mx = property.Body as MemberExpression;
            var propertyInfo = mx?.Member as PropertyInfo
                ?? throw new ArgumentException($"The lambda expression '{nameof(property)}' should point to a valid Property.");

            var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            var jsonName = jsonProperty.PropertyName;

            var clone = Clone();
            clone.options.FieldList.Add(jsonName);

            return clone;
        }

        // public Filter<TModel, TSortable, TFilterable> OrderBy<TValue>(Expression<Func<TSortable, TValue>> property)
        // {
        // }

        // public Filter<TModel, TSortable, TFilterable> OrderByDescending<TValue>(Expression<Func<TSortable, TValue>> property)
        // {
        // }

        // public Filter<TModel, TSortable, TFilterable> WithFilter<TValue>(Expression<Func<TFilterable, TValue>> property, TValue value)
        // {
        // }

        public Filter<TModel, TSortable, TFilterable> Skip(int offset)
        {
            var clone = Clone();
            clone.options.Offset = offset;
            return clone;
        }

        public Filter<TModel, TSortable, TFilterable> Take(int limit)
        {
            var clone = Clone();
            clone.options.Limit = limit;
            return clone;
        }

        public async Task<TModel> ElementAtAsync(int index)
        {
            var result = await ElementAtOrDefaultAsync(index).ConfigureAwait(false);
            return result ?? throw new ArgumentOutOfRangeException(nameof(index));
        }

        public Task<TModel> ElementAtOrDefaultAsync(int index) =>
             Skip(index).Take(1).FirstOrDefaultAsync();

        public async Task<TModel> SingleAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.Single();
        }

        public async Task<TModel> SingleOrDefaultAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.SingleOrDefault();
        }

        public async Task<TModel> FirstAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.First();
        }

        public async Task<TModel> FirstOrDefaultAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.FirstOrDefault();
        }

        public async Task<int> CountAsync()
        {
            var count = await client.CountAsync<TModel>().ConfigureAwait(false);
            return (int)count;
        }

        public Task<long> LongCountAsync() =>
            client.CountAsync<TModel>();

        public async Task<List<TModel>> ToListAsync()
        {
            var results = await GetCollectionResultAsync().ConfigureAwait(false);
            return results.ToList();
        }

        public async Task<TModel[]> ToArrayAsync()
        {
            var results = await GetCollectionResultAsync().ConfigureAwait(false);
            return results.ToArray();
        }

        private async Task<IEnumerable<TModel>> GetCollectionResultAsync() =>
            await client.FilterAsync<TModel>(options).ConfigureAwait(false);

        private Task<IEnumerable<TModel>> GetSingleResultAsync() =>
            Take(1).GetCollectionResultAsync();

        private Filter<TModel, TSortable, TFilterable> Clone() =>
            new Filter<TModel, TSortable, TFilterable>(client, new FilterOptions(options));
    }
}
