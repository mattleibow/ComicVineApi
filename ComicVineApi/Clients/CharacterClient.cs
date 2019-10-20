using System.Collections.Generic;
using System.Threading.Tasks;
using ComicVineApi.Http;
using ComicVineApi.Models;

namespace ComicVineApi.Clients
{
    public class CharacterClient : ClientBase
    {
        public CharacterClient(IApiConnection connection)
            : base(connection, 4005, "characters", "character")
        {
        }

        public Filter<Character, ICharacterSortable, ICharacterFilterable> Filter() =>
            new Filter<Character, ICharacterSortable, ICharacterFilterable>(this);

        public Task<IReadOnlyList<Character>> FilterAsync(FilterOptions options) =>
            FilterAsync<Character>(options);

        public Task<CharacterDetailed> GetAsync(int id) =>
            GetAsync<CharacterDetailed>(id);
    }
}
