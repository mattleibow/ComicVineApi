using System;

namespace ComicVineApi.Tests
{
    public interface ITestModelFilterable
    {
        int? Id { get; set; }

        string? FirstName { get; set; }

        DateTimeOffset? NullableDate { get; set; }

        DateTimeOffset Date { get; set; }

        string? LastName { get; set; }
    }
}
