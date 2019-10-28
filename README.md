# Comic Vine API

[![Build Status](https://dev.azure.com/mattleibow/OpenSource/_apis/build/status/ComicVineApi?branchName=master)](https://dev.azure.com/mattleibow/OpenSource/_build/latest?definitionId=24&branchName=master)

A beautiful .NET API that wraps the awesome Comic Vine REST API.

## Requirements

You will need an API key from https://comicvine.com/api.

## Usage

There are 3 main ways to find/fetch information:

 1. Request a resource by ID
 2. Filter a specific type of resource
 3. Search for any resource that matches a query

### 1. Request

For each of the resources, you can esily request a specific item in 1 line of code:

```csharp
// create the client
var client = new ComicVineClient("<API-KEY>", "<CUSTOM-USER-AGENT>");

// request the Batgirl (Cassandra Cain) character
var cassie = await client.Character.GetAsync(65230);

// print something out
Console.WriteLine($"{cassie.Name} was born on {cassie.Birth}.");
```

### 2. Filter

Basic filter with values:

```csharp
// create the client
var client = new ComicVineClient("<API-KEY>", "<CUSTOM-USER-AGENT>");

// request the female Cain family characters
var females = client.Character.Filter()
    .WithValue(x => x.Name, "Cain")
    .WithValue(x => x.Gender, Gender.Female);

// fetch the first result (all fields)
var first = await females.FirstOrDefaultAsync();

// print something out
Console.WriteLine($"{first.Name} was born on {first.Birth}.");
```

Filter with reduced fields for smaller payloads:

```csharp
// create the client
var client = new ComicVineClient("<API-KEY>", "<CUSTOM-USER-AGENT>");

// request the female Cain family characters
var females = client.Character.Filter()
    .WithValue(x => x.Name, "Cain")
    .WithValue(x => x.Gender, Gender.Female);

// just fetch minimal data (id, name, birth)
var smallPayload = females
    .IncludeField(x => x.Id)
    .IncludeField(x => x.Name)
    .IncludeField(x => x.Birth);

// fetch all the results on the page
var page = await smallPayload.ToListAsync();

// fetch all the items on all the pages
foreach (var character in page)
{
    // print something out
    Console.WriteLine($"{character.Name} was born on {character.Birth}.");
}
```

Iterate over all the results on the server (via multiple API calls):

```csharp
// create the client
var client = new ComicVineClient("<API-KEY>", "<CUSTOM-USER-AGENT>");

// request the female Cain family characters
var females = client.Character.Filter()
    .WithValue(x => x.Name, "Cain")
    .WithValue(x => x.Gender, Gender.Female);

// fetch all the items on all the pages
await foreach (var character in filter.ToAsyncEnumerable())
{
    // print something out
    Console.WriteLine($"{character.Name} was born on {character.Birth}.");
}
```

### 3. Search

> TODO: add docs

## Progress

> TODO: add docs

- [X] Search
- [ ] Resources
  - [X] Characters
  - [ ] Chats
  - [ ] Concepts
  - [ ] Episodes
  - [ ] Issues
  - [ ] Locations
  - [ ] Movies
  - [ ] Objects
  - [X] Origins
  - [ ] People
  - [ ] Powers
  - [ ] Promos
  - [X] Publishers
  - [X] Series
  - [ ] Story Arcs
  - [ ] Teams
  - [ ] Videos
  - [ ] Video Types
  - [ ] Video Categories
  - [X] Volumes
