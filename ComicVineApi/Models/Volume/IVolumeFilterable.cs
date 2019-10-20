using System;

namespace ComicVineApi.Models
{
    public interface IVolumeFilterable
    {
        DateTimeOffset? DateAdded { get; set; }

        DateTimeOffset? DateLastUpdated { get; set; }

        int? Id { get; set; }

        string? Name { get; set; }
    }
}
