using DataLayer.Contexts;
using DataLayer.FetchDTO.Ability;
using DataLayer.FetchDTO.Move;
using DataLayer.FetchDTO.Types;
using DataLayer.Services.PokeApiFetcher;
using DataLayer.Services.PokemonSeedHandler;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services.DbSeeder;

public class DbSeeder : IDbSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly IPokeApiFetcher _pokeApiFetcher;
    private readonly IPokemonSeedHandler _pokemonSeedHandler;

    public DbSeeder(AppDbContext dbContext, IPokemonSeedHandler pokemonSeedHandler, IPokeApiFetcher pokeApiFetcher)
    {
        _dbContext = dbContext;
        _pokemonSeedHandler = pokemonSeedHandler;
        _pokeApiFetcher = pokeApiFetcher;
    }

    public async Task SeedAllEntitiesAsync()
    {
        var allAbilityIds = await _dbContext.Abilities
            .Select(i => i.Id)
            .ToListAsync();

        var allTypeIds = await _dbContext.Types
            .Select(i => i.Id)
            .ToListAsync();

        var allMoveIds = await _dbContext.Moves
            .Select(i => i.Id)
            .ToListAsync();

        var allPokemonIds = await _dbContext.Pokemons
            .Select(i => i.Id)
            .ToListAsync();

        var pokemons = _pokeApiFetcher.FetchAllPokemons();
        var abilities = _pokeApiFetcher.FetchAllAbilities();
        var types = _pokeApiFetcher.FetchAllTypes();
        var moves = _pokeApiFetcher.FetchAllMoves();

        Console.WriteLine("All entities fetched");

        foreach (var ability in abilities.Where(ability => !allAbilityIds.Contains(ability.Id)))
            await _dbContext.Abilities.AddAsync(ability);

        foreach (var type in types.Where(type => !allTypeIds.Contains(type.Id)))
            await _dbContext.Types.AddAsync(type);

        foreach (var move in moves.Where(move => !allMoveIds.Contains(move.Id)))
            await _dbContext.Moves.AddAsync(move);
        
        var pokemonAbilityRelationships = _pokemonSeedHandler
            .GetPokemonAbilityRelationships(pokemons, abilities);

        var pokemonMoveRelationships = _pokemonSeedHandler
            .GetPokemonMoveRelationships(pokemons, moves);

        var pokemonTypeRelationships = _pokemonSeedHandler
            .GetPokemonTypeRelationships(pokemons, types);
        
        foreach (var pokemon in pokemons)
        {
            pokemon.Types = null;
            pokemon.Moves = null;
            pokemon.Abilities = null;
        }
        
        foreach (var pokemon in pokemons.Where(pokemon => !allPokemonIds.Contains(pokemon.Id)))
            await _dbContext.Pokemons.AddAsync(pokemon);

        await _dbContext.SaveChangesAsync();
        Console.WriteLine("All entities loaded to database");
        
        await SeedRelationships(pokemonAbilityRelationships, pokemonMoveRelationships, pokemonTypeRelationships);
    }

    private async Task SeedRelationships(
        List<PokemonAbilityRelationship> pokemonAbilityRelationships,
        List<PokemonMoveRelationship> pokemonMoveRelationships,
        List<PokemonTypeRelationship> pokemonTypeRelationships)
    {
        var allPokemons = await _dbContext.Pokemons
            .Include(i => i.Abilities)
            .Include(i => i.Moves)
            .Include(i => i.Types)
            .ToListAsync();

        var allAbilities = await _dbContext.Abilities
            .ToListAsync();

        var allMoves = await _dbContext.Moves
            .ToListAsync();

        var allTypes = await _dbContext.Types
            .ToListAsync();

        var allPokemonAbilityRelationships = new List<Tuple<int, int>>();
        var allPokemonMoveRelationships = new List<Tuple<int, int>>();
        var allPokemonTypeRelationships = new List<Tuple<int, int>>();

        foreach (var pokemon in allPokemons)
        {
            Console.WriteLine($"Configuring pokemon: ${pokemon.Name}");
            
            var oldAbilityIds = pokemon.Abilities
                .Select(ability => ability.Id);

            var abilityIdsToAdd = pokemonAbilityRelationships
                .Where(i => i.PokemonId == pokemon.Id)
                .Where(i => !oldAbilityIds.Contains(i.AbilityId))
                .Select(i => i.AbilityId);

            var abilitiesToAdd = allAbilities
                .Where(i => abilityIdsToAdd.Contains(i.Id));

            pokemon.Abilities.AddRange(abilitiesToAdd);

            var oldMoveIds = pokemon.Moves
                .Select(move => move.Id);

            var moveIdsToAdd = pokemonMoveRelationships
                .Where(i => i.PokemonId == pokemon.Id)
                .Where(i => !oldMoveIds.Contains(i.MoveId))
                .Select(i => i.MoveId);

            var movesToAdd = allMoves
                .Where(i => moveIdsToAdd.Contains(i.Id));

            pokemon.Moves.AddRange(movesToAdd);

            var oldTypeIds = pokemon.Types
                .Select(type => type.Id);

            var typeIdsToAdd = pokemonTypeRelationships
                .Where(i => i.PokemonId == pokemon.Id)
                .Where(i => !oldTypeIds.Contains(i.TypeId))
                .Select(i => i.TypeId);

            var typesToAdd = allTypes
                .Where(i => typeIdsToAdd.Contains(i.Id));

            pokemon.Types.AddRange(typesToAdd);
        }

        await _dbContext.SaveChangesAsync();
        Console.WriteLine("All relationships loaded to database");
    }
}