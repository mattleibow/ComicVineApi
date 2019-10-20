using System;

namespace ComicVineApi.Models
{
    public interface IIssueSortable
    {
        DateTimeOffset? CoverDate { get; set; }

        DateTimeOffset? DateAdded { get; set; }

        DateTimeOffset? DateLastUpdated { get; set; }

        int? Id { get; set; }

        string? IssueNumber { get; set; }

        string? Name { get; set; }

        DateTimeOffset? StoreDate { get; set; }
    }
}
