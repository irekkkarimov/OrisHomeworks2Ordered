using System.Net;
using Newtonsoft.Json;
using PokemonAPI.Models;
using PokemonAPI.Models.DTOs;
using PokemonAPI.Models.DTOs.ResponseDTOs;
using PokemonAPI.Services.PokemonCacheHandler;

namespace PokemonAPI.Services.PokeApiService;

public class PokeApiService : IPokeApiService
{
    private readonly IPokemonCacheHandler _pokemonCacheHandler;
    private readonly HttpClient _httpClient = new();
    private readonly string? _pokeApiPokemonUrl;
    
    public PokeApiService(IConfiguration configuration, IPokemonCacheHandler pokemonCacheHandler)
    {
        _pokemonCacheHandler = pokemonCacheHandler;
        _pokeApiPokemonUrl = configuration.GetSection("Other")["PokeApiPokemonUrl"];
    }
        
    public async Task<PokemonResponse> GetByFilterAsync(string filter = "", int limit = 20, int offset = 0)
    {
        if (limit == 0)
            limit = 20;
        
        var response = await _httpClient.GetStringAsync(_pokeApiPokemonUrl + "?limit=10000");
        var pokemonList = JsonConvert.DeserializeObject<PokeApiRequestDto>(response);

        if (pokemonList is null)
            throw new NullReferenceException("Pokemons are not existing anymore!");
        
        var pokemonListFiltered = pokemonList.Results
            .Where(i => i.Name.ToLower().Contains(filter.ToLower()))
            .ToList();

        var count = pokemonListFiltered.Count;
        
        var pokemonsToReturn = pokemonListFiltered
            .Skip(offset)
            .Take(limit)
            .ToList();
        
        var pokemonDetailedList = new List<PokemonDetailed>();
        foreach (var pokemon in pokemonsToReturn)
        {
            var pokemonFromCache = await _pokemonCacheHandler.GetByName(pokemon.Name);
            if (pokemonFromCache is not null)
            {
                Console.WriteLine("Returned from cache");
                pokemonDetailedList.Add(pokemonFromCache);
                continue;
            }
            
            var pokemonDetailed = await _httpClient.GetFromJsonAsync<PokemonDetailed>(pokemon.Url);

            if (pokemonDetailed is null)
                throw new NullReferenceException("Pokemon does not exist!");
            pokemonDetailedList.Add(pokemonDetailed);
            await _pokemonCacheHandler.Add(pokemonDetailed);
        }

        var pokemonResponseDtoList = pokemonDetailedList
            .Select(MapPokemonDetailedToPokemonResponseDto)
            .ToList();

        return new PokemonResponse
        {
            Results = pokemonResponseDtoList,
            Count = count
        };
    }

    public async Task<PokemonDetailed?> GetByIdOrNameAsync(string idOrName)
    {
        var isParameterId = int.TryParse(idOrName, out var id);
        var pokemonFromCache = isParameterId
            ? await _pokemonCacheHandler.GetById(id)
            : await _pokemonCacheHandler.GetByName(idOrName);

        if (pokemonFromCache is not null)
        {
            Console.WriteLine("Returned from cache");
            return pokemonFromCache;
        }
        
        var response = await _httpClient.GetAsync(_pokeApiPokemonUrl + $"/{idOrName.ToLower()}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        var responseData = await response.Content.ReadAsStringAsync();
        var pokemonDetailed = JsonConvert.DeserializeObject<PokemonDetailed>(responseData);
        
        if (pokemonDetailed is not null)
            await _pokemonCacheHandler.Add(pokemonDetailed);

        return pokemonDetailed;
    }

    private PokemonResponseItem MapPokemonDetailedToPokemonResponseDto(PokemonDetailed pokemonDetailed)
    {
        return new PokemonResponseItem
        {
            Id = pokemonDetailed.Id,
            Name = pokemonDetailed.Name,
            Sprites = pokemonDetailed.Sprites,
            Types = pokemonDetailed.Types
        };
    }
}