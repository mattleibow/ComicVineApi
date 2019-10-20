using System;
using System.Collections.Generic;

namespace ComicVineApi.Models
{
    public interface IIssueFilterable
    {
        IReadOnlyList<string>? Aliases { get; set; }

        DateTimeOffset? CoverDate { get; set; }

        DateTimeOffset? DateAdded { get; set; }

        DateTimeOffset? DateLastUpdated { get; set; }

        int? Id { get; set; }

        string? IssueNumber { get; set; }

        string? Name { get; set; }

        DateTimeOffset? StoreDate { get; set; }

        VolumeReference? Volume { get; set; }
    }
}
