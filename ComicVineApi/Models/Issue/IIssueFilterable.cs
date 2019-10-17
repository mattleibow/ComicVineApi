using System;

namespace ComicVineApi.Models
{
    public interface IIssueFilterable
    {
        string[] Aliases { get; set; }

        DateTimeOffset? CoverDate { get; set; }

        DateTimeOffset DateAdded { get; set; }

        DateTimeOffset DateLastUpdated { get; set; }

        long Id { get; set; }

        string IssueNumber { get; set; }

        string Name { get; set; }

        DateTimeOffset? StoreDate { get; set; }

        VolumeReference Volume { get; set; }
    }
}
