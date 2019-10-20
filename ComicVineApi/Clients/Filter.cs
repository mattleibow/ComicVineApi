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
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.FieldList.Add(jsonName);

            return clone;
        }

        public Filter<TModel, TSortable, TFilterable> OrderBy<TValue>(Expression<Func<TSortable, TValue>> property)
        {
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.SortField = jsonName;
            clone.options.SortDescending = false;

            return clone;
        }

        public Filter<TModel, TSortable, TFilterable> OrderByDescending<TValue>(Expression<Func<TSortable, TValue>> property)
        {
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.SortField = jsonName;
            clone.options.SortDescending = true;

            return clone;
        }

        public Filter<TModel, TSortable, TFilterable> WithValue<TValue>(Expression<Func<TFilterable, TValue>> property, TValue value)
        {
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.Filter.Add(jsonName, $"{value}");

            return clone;
        }

        public Filter<TModel, TSortable, TFilterable> WithValue(Expression<Func<TFilterable, DateTimeOffset>> property, DateTimeOffset startDate) =>
            WithDate(property, startDate, new DateTimeOffset(9999, 12, 31, 0, 0, 0, TimeSpan.Zero));

        public Filter<TModel, TSortable, TFilterable> WithValue(Expression<Func<TFilterable, DateTimeOffset>> property, DateTimeOffset startDate, DateTimeOffset endDate) =>
            WithDate(property, startDate, endDate);

        public Filter<TModel, TSortable, TFilterable> WithValue(Expression<Func<TFilterable, DateTimeOffset?>> property, DateTimeOffset startDate) =>
            WithDate(property, startDate, new DateTimeOffset(9999, 12, 31, 0, 0, 0, TimeSpan.Zero));

        public Filter<TModel, TSortable, TFilterable> WithValue(Expression<Func<TFilterable, DateTimeOffset?>> property, DateTimeOffset startDate, DateTimeOffset endDate) =>
            WithDate(property, startDate, endDate);

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

        public Task<int> CountAsync() =>
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

        private Filter<TModel, TSortable, TFilterable> WithDate<TDateType>(Expression<Func<TFilterable, TDateType>> property, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.Filter.Add(jsonName, $"{startDate.UtcDateTime:yyyy-MM-dd}|{endDate.UtcDateTime:yyyy-MM-dd}");

            return clone;
        }

        private static string GetPropertyName<TObject, TValue>(Expression<Func<TObject, TValue>> property)
        {
            _ = property ?? throw new ArgumentNullException(nameof(property));

            var mx = property.Body as MemberExpression;
            var propertyInfo = mx?.Member as PropertyInfo
                ?? throw new ArgumentException($"The lambda expression '{nameof(property)}' should point to a valid Property.");

            propertyInfo = typeof(TModel).GetRuntimeProperty(propertyInfo.Name);

            var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            var jsonName = jsonProperty.PropertyName;
            return jsonName;
        }

        private async Task<IEnumerable<TModel>> GetCollectionResultAsync() =>
            await client.FilterAsync<TModel>(options).ConfigureAwait(false);

        private Task<IEnumerable<TModel>> GetSingleResultAsync() =>
            Take(1).GetCollectionResultAsync();

        private Filter<TModel, TSortable, TFilterable> Clone() =>
            new Filter<TModel, TSortable, TFilterable>(client, new FilterOptions(options));
    }
}
