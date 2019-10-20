using System;

namespace ComicVineApi.Models
{
    public interface ICharacterFilterable
    {
        DateTimeOffset? DateAdded { get; set; }

        DateTimeOffset? DateLastUpdated { get; set; }

        Gender? Gender { get; set; }

        int? Id { get; set; }

        string? Name { get; set; }
    }
}
