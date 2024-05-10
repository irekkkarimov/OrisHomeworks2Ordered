using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Types;
using Domain.Entities;
using Ability = Domain.Entities.Ability;
using Move = Domain.Entities.Move;
using Type = Domain.Entities.Type;

namespace DataLayer.Services.PokemonSeedHandler;

public interface IPokemonSeedHandler
{
    List<Move> ConfigureMoves(List<Move> moves, List<Type> types);
    List<Pokemon> ConfigurePokemons(List<Pokemon> pokemons, List<Type> types, List<Move> moves, List<Ability> abilities);
    List<PokemonAbilityRelationship> GetPokemonAbilityRelationships(List<Pokemon> pokemons, List<Ability> abilities);
    List<PokemonMoveRelationship> GetPokemonMoveRelationships(List<Pokemon> pokemons, List<Move> moves);
    List<PokemonTypeRelationship> GetPokemonTypeRelationships(List<Pokemon> pokemons, List<Type> types);
}