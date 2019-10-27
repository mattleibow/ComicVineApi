using System;
using System.Collections.Generic;

namespace ComicVineApi.Clients
{
    public class SearchOptions
    {
        internal const int DefaultOffset = 0;
        internal const int DefaultLimit = 10;

        public SearchOptions(string query)
        {
            FieldList = new List<string>();
            Resources = new List<string>();
            Query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public SearchOptions(SearchOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            Offset = options.Offset;
            Limit = options.Limit;
            FieldList = new List<string>(options.FieldList);
            Resources = new List<string>(options.Resources);
            Query = options.Query;
        }

        public int Offset { get; set; } = DefaultOffset;

        public int Limit { get; set; } = DefaultLimit;

        public List<string> FieldList { get; }

        public List<string> Resources { get; }

        public string Query { get; }
    }

    public enum SearchResource
    {
        Character,
        Concept,
        Origin,
        Object,
        Location,
        Issue,
        StoryArc,
        Volume,
        Publisher,
        Person,
        Team,
        Video
    }
}
