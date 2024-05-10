using PokemonAPI.Models;
using PokemonAPI.Models.DTOs.ResponseDTOs;

namespace PokemonAPI.Services.PokeApiService;

public interface IPokeApiService
{
    /// <summary>
    /// Filters Pokemons by given filter
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="filter">Used for filter</param>
    /// <returns>List of <see cref="PokemonResponseItem"/></returns>
    /// <exception cref="NullReferenceException">Source list was empty or no Pokemon for the filter was found</exception>
    Task<PokemonResponse> GetByFilterAsync(string filter = "", int limit = 20, int offset = 0);
    
    /// <summary>
    /// Gets a pokemon with given id or name
    /// </summary>
    /// <param name="idOrName">Id or Name used for search</param>
    /// <returns><see cref="PokemonDetailed"/></returns>
    Task<PokemonDetailed?> GetByIdOrNameAsync(string idOrName);
}