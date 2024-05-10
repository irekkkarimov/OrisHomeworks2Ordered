using PokemonAPI.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace PokemonAPI.Services.PokemonCacheHandler;

public class PokemonCacheHandler : IPokemonCacheHandler
{
    private readonly RedisCollection<PokemonDetailed> _pokemonCollection;

    public PokemonCacheHandler(RedisConnectionProvider provider)
    {
        _pokemonCollection = (RedisCollection<PokemonDetailed>)provider.RedisCollection<PokemonDetailed>();
    }

    public async Task Add(PokemonDetailed pokemonDetailed)
    {
        await _pokemonCollection.InsertAsync(pokemonDetailed);
    }

    public async Task<PokemonDetailed?> GetById(int id)
    {
        return await _pokemonCollection.FindByIdAsync(id.ToString());
    }

    public async Task<PokemonDetailed?> GetByName(string name)
    {
        name = name.ToLower();
        return await _pokemonCollection.FirstOrDefaultAsync(i => i.Name == name);
    }

    // public async Task<IList<PokemonDetailed>> GetByNameFilter(string filter)
    // {
    //     filter = filter.ToLower();
    //     return await _pokemonCollection
    //         .Where(i => i.Name.Contains(filter))
    //         .ToListAsync();
    // }
}