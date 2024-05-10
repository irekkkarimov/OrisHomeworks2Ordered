using System.Net;
using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Types;
using Domain.Entities;
using Ability = Domain.Entities.Ability;
using Move = Domain.Entities.Move;
using Type = Domain.Entities.Type;

namespace DataLayer.Services.PokemonSeedHandler;

public class PokemonSeedHandler : IPokemonSeedHandler
{
    public List<Move> ConfigureMoves(List<Move> moves, List<Type> types)
    {
        foreach (var move in moves)
        {
            var newType = types.FirstOrDefault(i => i.Id == move.TypeId);
            if (newType is null)
                throw new NullReferenceException("Type is null");

            move.Type = null;
        }

        return moves;
    }

    public List<Pokemon> ConfigurePokemons(List<Pokemon> pokemons, List<Type> types, List<Move> moves,
        List<Ability> abilities)
    {
        foreach (var pokemon in pokemons)
        {
            var newAbilities = abilities
                .Where(i => pokemon.Abilities.Select(e => e.Id).Contains(i.Id))
                .ToList();

            var newTypes = types
                .Where(i => pokemon.Types.Select(e => e.Id).Contains(i.Id))
                .ToList();

            var newMoves = moves
                .Where(i => pokemon.Moves.Select(e => e.Id).Contains(i.Id))
                .ToList();

            pokemon.Abilities = newAbilities;
            pokemon.Types = newTypes;
            pokemon.Moves = newMoves;
        }

        return pokemons;
    }

    public List<PokemonAbilityRelationship> GetPokemonAbilityRelationships(List<Pokemon> pokemons,
        List<Ability> abilities)
    {
        return pokemons
            .SelectMany(pokemon =>
            {
                var abilitiesUsed = new List<int>();
                return pokemon.Abilities
                    .Where(ability =>
                    {
                        if (abilitiesUsed.Contains(ability.Id))
                            return false;

                        abilitiesUsed.Add(ability.Id);
                        return true;
                    })
                    .Select(ability =>
                        new PokemonAbilityRelationship
                        {
                            PokemonId = pokemon.Id,
                            AbilityId = ability.Id
                        })
                    .ToList();
            })
            .ToList();
    }

    public List<PokemonMoveRelationship> GetPokemonMoveRelationships(List<Pokemon> pokemons, List<Move> moves)
    {
        return pokemons
            .SelectMany(pokemon =>
                pokemon.Moves
                    .Select(move =>
                        new PokemonMoveRelationship
                        {
                            PokemonId = pokemon.Id,
                            MoveId = move.Id
                        })
                    .ToList())
            .ToList();
    }

    public List<PokemonTypeRelationship> GetPokemonTypeRelationships(List<Pokemon> pokemons, List<Type> types)
    {
        return pokemons
            .SelectMany(pokemon =>
                pokemon.Types
                    .Select(type =>
                        new PokemonTypeRelationship
                        {
                            PokemonId = pokemon.Id,
                            TypeId = type.Id
                        })
                    .ToList())
            .ToList();
    }
}