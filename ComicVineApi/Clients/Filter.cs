using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComicVineApi.Clients
{
    public class Filter<TModel, TSortable, TFilterable>
        where TModel : TSortable, TFilterable
    {
        protected const int DefaultPageNumber = 0;
        protected const int DefaultPageSize = 100;

        private readonly ClientBase client;

        private HashSet<string> includes = new HashSet<string>();

        public Filter(ClientBase client)
        {
            this.client = client;
        }

        public Filter<TModel, TSortable, TFilterable> Include<TValue>(Expression<Func<TModel, TValue>> property)
        {
            var mx = property.Body as MemberExpression;
            var propertyInfo = mx.Member as PropertyInfo
                ?? throw new ArgumentException ($"The lambda expression '{nameof(property)}' should point to a valid Property.");

            var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            var jsonName = jsonProperty.PropertyName;

            var clone = Clone();
            clone.includes.Add(jsonName);

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

        public Task<IReadOnlyList<TModel>> FetchAsync(int page = DefaultPageNumber, int count = DefaultPageSize) =>
            client.FilterAsync<TModel>(new FilterOptions { PageNumber = page, PageSize = count });

        public Filter<TModel, TSortable, TFilterable> Clone()
        {
            var filter = new Filter<TModel, TSortable, TFilterable>(client);
            filter.includes = new HashSet<string>(includes);
            return filter;
        }
    }
}
