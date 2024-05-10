using PokemonAPI.Models;

namespace PokemonAPI.Services.PokemonCacheHandler;

public interface IPokemonCacheHandler
{
    /// <summary>
    /// Adds <see cref="PokemonDetailed"/> to cache
    /// </summary>
    /// <param name="pokemonDetailed">Pokemon to be added to cache</param>
    Task Add(PokemonDetailed pokemonDetailed);
    
    /// <summary>
    /// Gets a <see cref="PokemonDetailed"/> by given id
    /// </summary>
    /// <param name="id">Id of Pokemon</param>
    /// <returns><see cref="PokemonDetailed"/> with given id</returns>
    Task<PokemonDetailed?> GetById(int id);
    
    /// <summary>
    /// Gets a Pokemon by given name
    /// </summary>
    /// <param name="name">Name of Pokemon</param>
    /// <returns><see cref="PokemonDetailed"/> with given name</returns>
    Task<PokemonDetailed?> GetByName(string name);
}