using System;
using System.Collections.Generic;

namespace ComicVineApi.Clients
{
    public class FilterOptions
    {
        internal const int DefaultOffset = 0;
        internal const int DefaultLimit = 100;

        public FilterOptions()
        {
            FieldList = new List<string>();
            Filter = new Dictionary<string, string>();
        }

        public FilterOptions(FilterOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            Offset = options.Offset;
            Limit = options.Limit;
            FieldList = new List<string>(options.FieldList);
            SortField = options.SortField;
            SortDescending = options.SortDescending;
            Filter = new Dictionary<string, string>(options.Filter);
        }

        public int Offset { get; set; } = DefaultOffset;

        public int Limit { get; set; } = DefaultLimit;

        public List<string> FieldList { get; }

        public string? SortField { get; set; }

        public bool SortDescending { get; set; }

        public Dictionary<string, string> Filter { get; }
    }
}
