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
        }

        public FilterOptions(FilterOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            Offset = options.Offset;
            Limit = options.Limit;
            FieldList.AddRange(options.FieldList);
        }

        public int Offset { get; set; } = DefaultOffset;

        public int Limit { get; set; } = DefaultLimit;

        public List<string> FieldList { get; } = new List<string>();
    }
}
