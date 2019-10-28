using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using ComicVineApi.Models;
using Newtonsoft.Json;

namespace ComicVineApi.Clients
{
    public class Search
    {
        private readonly SearchClient client;
        private readonly SearchOptions options;

        internal Search(SearchClient client, string query)
            : this(client, new SearchOptions(query))
        {
        }

        internal Search(SearchClient client, SearchOptions options)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Search IncludeField<TModel>(Expression<Func<TModel, object?>> property)
        {
            var jsonName = GetPropertyName(property);

            var clone = Clone();
            clone.options.FieldList.Add(jsonName);

            return clone;
        }

        public Search IncludeResource(SearchResource resource)
        {
            var resourceName = GetResourceName(resource);

            var clone = Clone();
            clone.options.Resources.Add(resourceName);

            return clone;
        }

        public Search Skip(int offset)
        {
            var clone = Clone();
            clone.options.Offset = offset;
            return clone;
        }

        public Search Take(int limit)
        {
            var clone = Clone();
            clone.options.Limit = limit;
            return clone;
        }

        public async Task<ComicVineObject> ElementAtAsync(int index)
        {
            var result = await ElementAtOrDefaultAsync(index).ConfigureAwait(false);
            return result ?? throw new ArgumentOutOfRangeException(nameof(index));
        }

        public Task<ComicVineObject> ElementAtOrDefaultAsync(int index) =>
             Skip(index).Take(1).FirstOrDefaultAsync();

        public async Task<ComicVineObject> SingleAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.Single();
        }

        public async Task<ComicVineObject> SingleOrDefaultAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.SingleOrDefault();
        }

        public async Task<ComicVineObject> FirstAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.First();
        }

        public async Task<ComicVineObject> FirstOrDefaultAsync()
        {
            var results = await GetSingleResultAsync().ConfigureAwait(false);
            return results.FirstOrDefault();
        }

        public Task<int> CountAsync() =>
            client.CountAsync(options);

        public async Task<List<ComicVineObject>> ToListAsync()
        {
            var results = await GetCollectionResultAsync().ConfigureAwait(false);
            return results.ToList();
        }

        public async Task<ComicVineObject[]> ToArrayAsync()
        {
            var results = await GetCollectionResultAsync().ConfigureAwait(false);
            return results.ToArray();
        }

        public SearchOptions ToOptions() =>
            new SearchOptions(options);

        public async IAsyncEnumerable<ComicVineObject> ToAsyncEnumerable()
        {
            var opt = new SearchOptions(options);

            // don't request more than we need
            while (opt.Limit > 0)
            {
                var results = await client.SearchAsync(opt).ConfigureAwait(false);

                // no more items in the source
                if (results.Count == 0)
                    yield break;

                // just return the requested amount
                var limited = results.Take(opt.Limit);
                foreach (var res in limited)
                    yield return res;

                opt.Offset += results.Count;
                opt.Limit -= results.Count;
            }
        }

        private static string GetPropertyName<TModel>(Expression<Func<TModel, object?>> property)
        {
            _ = property ?? throw new ArgumentNullException(nameof(property));

            var mx = property.Body as MemberExpression;
            if (mx == null)
            {
                var unary = property.Body as UnaryExpression;
                if (unary?.NodeType == ExpressionType.Convert)
                    mx = unary.Operand as MemberExpression;
            }
            var propertyInfo = mx?.Member as PropertyInfo
                ?? throw new ArgumentException($"The lambda expression '{property}' should point to a valid Property.");

            propertyInfo = typeof(TModel).GetRuntimeProperty(propertyInfo.Name);

            var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            var jsonName = jsonProperty.PropertyName;
            return jsonName;
        }

        private string GetResourceName(SearchResource resource) =>
            resource switch
            {
                SearchResource.Character => "character",
                SearchResource.Concept => "concept",
                SearchResource.Origin => "origin",
                SearchResource.Object => "object",
                SearchResource.Location => "location",
                SearchResource.Issue => "issue",
                SearchResource.StoryArc => "story_arc",
                SearchResource.Volume => "volume",
                SearchResource.Publisher => "publisher",
                SearchResource.Person => "person",
                SearchResource.Team => "team",
                SearchResource.Video => "video",
                _ => throw new ArgumentOutOfRangeException(nameof(resource)),
            };

        private async Task<IEnumerable<ComicVineObject>> GetCollectionResultAsync() =>
            await client.SearchAsync(options).ConfigureAwait(false);

        private Task<IEnumerable<ComicVineObject>> GetSingleResultAsync() =>
            Take(1).GetCollectionResultAsync();

        private Search Clone() =>
            new Search(client, new SearchOptions(options));
    }
}
