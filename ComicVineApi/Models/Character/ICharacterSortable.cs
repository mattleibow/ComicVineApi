using System;

namespace ComicVineApi.Models
{
    public interface ICharacterSortable
    {
        DateTimeOffset DateAdded { get; set; }

        DateTimeOffset DateLastUpdated { get; set; }

        long Id { get; set; }

        string Name { get; set; }
    }
}
