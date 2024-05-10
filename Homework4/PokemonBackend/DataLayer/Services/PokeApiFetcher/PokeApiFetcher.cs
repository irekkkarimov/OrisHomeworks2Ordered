using DataLayer.FetchDTO;
using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Pokemon;
using DataLayer.FetchDTO.Types;
using Domain.Entities;
using Newtonsoft.Json;
using Ability = Domain.Entities.Ability;
using Move = Domain.Entities.Move;
using Type = Domain.Entities.Type;

namespace DataLayer.Services.PokeApiFetcher;

public class PokeApiFetcher : IPokeApiFetcher
{
    private const string Url = "https://pokeapi.co/api/v2/";
    private readonly HttpClient _httpClient = new();

    public List<Pokemon> FetchAllPokemons()
    {
        const string specificUrl = Url + "pokemon" + "?limit=10000";
        var responseResult = _httpClient.GetStringAsync(specificUrl).Result;
        var pokemonsReceived = JsonConvert.DeserializeObject<PokeApiRequestDto>(responseResult);

        if (pokemonsReceived is null)
            throw new NullReferenceException("Pokemons not found");

        var pokemons = pokemonsReceived.Results;
        var pokemonDetailedFetchList = new List<PokemonDetailedFetch>();

        foreach (var newPokemon in pokemons
                     .Select(pokemon => JsonConvert.DeserializeObject<PokemonDetailedFetch>(_httpClient
                         .GetStringAsync(pokemon.Url)
                         .Result)))
        {
            if (newPokemon is null) throw new NullReferenceException("Pokemon not found");
            pokemonDetailedFetchList.Add(newPokemon);
        }

        var pokemonsResult = pokemonDetailedFetchList
            .Select(i =>
                new Pokemon
                {
                    Id = i.Id,
                    Name = i.Name,
                    Image = i.Sprites.Other.Home.Front_Default != ""
                        ? i.Sprites.Other.Home.Front_Default
                        : null,
                    Height = i.Height,
                    Weight = i.Weight,
                    Hp = i.Stats.FirstOrDefault(i => i.Stat.Name.Equals("hp")).Base_Stat,
                    Attack = i.Stats.FirstOrDefault(i => i.Stat.Name.Equals("attack")).Base_Stat,
                    Defense = i.Stats.FirstOrDefault(i => i.Stat.Name.Equals("defense")).Base_Stat,
                    Speed = i.Stats.FirstOrDefault(i => i.Stat.Name.Equals("speed")).Base_Stat,
                    Types = i.Types
                        .Select(e => e.Type)
                        .Select(e => new Type
                        {
                            Id = int.Parse(e.Url.Split('/')[^2]),
                            Name = e.Name
                        }).ToList(),
                    Moves = i.Moves
                        .Select(e => e.Move)
                        .Select(e => new Move
                        {
                            Id = int.Parse(e.Url.Split('/')[^2]),
                            Name = e.Name
                        }).ToList(),
                    Abilities = i.Abilities
                        .Select(e => e.Ability)
                        .Select(e => new Ability
                        {
                            Id = int.Parse(e.Url.Split('/')[^2]),
                            Name = e.Name
                        }).ToList()
                }
            ).ToList();

        return pokemonsResult;
    }

    public List<Type> FetchAllTypes()
    {
        const string specificUrl = Url + "type" + "?limit=10000";
        var responseResult = _httpClient.GetStringAsync(specificUrl).Result;
        var typesReceived = JsonConvert.DeserializeObject<TypesFetchDto>(responseResult);

        if (typesReceived is null)
            throw new NullReferenceException("Pokemons not found");

        var typeDtoList = typesReceived.Results;
        
        foreach (var typesReceivedResult in typesReceived.Results)
        {
            Console.WriteLine(typesReceivedResult.Url);
            Console.WriteLine(int.Parse(typesReceivedResult.Url.Split('/')[^2]));
        }

        var typeList = typeDtoList
            .Select(typeDto =>
                new Type
                {
                    Id = int.Parse(typeDto.Url.Split('/')[^2]),
                    Name = typeDto.Name,
                    Pokemons = new List<Pokemon>()
                })
            .ToList();

        return typeList;
    }

    public List<Move> FetchAllMoves()
    {
        const string specificUrl = Url + "move" + "?limit=10000";
        var responseResult = _httpClient.GetStringAsync(specificUrl).Result;
        var movesReceived = JsonConvert.DeserializeObject<MovesFetchDto>(responseResult);

        if (movesReceived is null || !movesReceived.Results.Any())
            throw new NullReferenceException("Moves not found");

        var moveDtoList = movesReceived.Results;

        var moveList = moveDtoList
            .Select(moveDto =>
            {
                var moveFetch = _httpClient.GetStringAsync(moveDto.Url).Result;
                var moveFetchDto = JsonConvert.DeserializeObject<FetchDTO.Move.Move>(moveFetch);
                if (moveFetchDto?.Type is null)
                    throw new NullReferenceException("Move or its Type not found");

                return new Move
                {
                    Id = int.Parse(moveDto.Url.Split('/')[^2]),
                    Name = moveDto.Name,
                    Pokemons = new List<Pokemon>(),
                    TypeId = int.Parse(moveFetchDto.Type.Url.Split('/')[^2]),
                    Type = null
                };
            })
            .ToList();

        return moveList;
    }

    public List<Ability> FetchAllAbilities()
    {
        const string specificUrl = Url + "ability" + "?limit=10000";
        var responseResult = _httpClient.GetStringAsync(specificUrl).Result;
        var abilitiesReceived = JsonConvert.DeserializeObject<AbilitiesFetchDto>(responseResult);

        if (abilitiesReceived is null || !abilitiesReceived.Results.Any())
            throw new NullReferenceException("Abilities not found");

        var abilityDtoList = abilitiesReceived.Results;

        var abilityList = abilityDtoList
            .Select(abilityDto =>
                new Ability
                {
                    Id = int.Parse(abilityDto.Url.Split('/')[^2]),
                    Name = abilityDto.Name,
                    Pokemons = new List<Pokemon>()
                })
            .ToList();

        return abilityList;
    }
}