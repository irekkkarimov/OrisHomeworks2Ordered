using AutoMapper;
using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO.Pokemon;
using PokemonAPI.Models.DTOs.ResponseDTOs;

namespace PokemonAPI.Services.PokemonService;

public class PokemonService : IPokemonService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PokemonService(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PokemonLessListGetDto> GetAllAsync(int limit, int offset)
    {
        var pokemonsCount = await _context.Pokemons.CountAsync();
        var pokemons = await _context.Pokemons
            .Skip(offset)
            .Take(limit)
            .Include(i => i.Types)
            .Include(i => i.Moves)
            .Include(i => i.Abilities)
            .ToListAsync();
        
        return new PokemonLessListGetDto
        {
            Count = pokemonsCount,
            Results = pokemons
                .Select(i => _mapper.Map<PokemonLessGetDto>(i))
                .ToList()
        };
    }

    public async Task<PokemonLessListGetDto> GetByFilterAsync(string filter, int limit, int offset)
    {
        var pokemonsByFilter = _context.Pokemons
            .Where(i => i.Name.ToLower().Contains(filter.ToLower()));

        var pokemonCount = await pokemonsByFilter.CountAsync();
        var pokemonsLimited = await pokemonsByFilter
            .Skip(offset)
            .Take(limit)
            .Include(i => i.Types)
            .Include(i => i.Moves)
            .Include(i => i.Abilities)
            .ToListAsync();

        return new PokemonLessListGetDto
        {
            Count = pokemonCount,
            Results = pokemonsLimited
                .Select(i => _mapper.Map<PokemonLessGetDto>(i))
                .ToList()
        };
    }

    public async Task<PokemonGetDto> GetByIdOrNameAsync(string idOrName)
    {
        Pokemon? pokemon;
        if (int.TryParse(idOrName, out var id))
            pokemon = await GetById(id);
        else
            pokemon = await GetByName(idOrName);

        if (pokemon is null)
            throw new NullReferenceException("Pokemon not found");

        var pokemonGetDto = _mapper.Map<PokemonGetDto>(pokemon);
        Console.WriteLine(pokemon.Types.Count);

        return pokemonGetDto;
    }

    private async Task<Pokemon?> GetById(int id)
    {
        return await _context.Pokemons
            .Include(i => i.Types)
            .Include(i => i.Moves)
            .Include(i => i.Abilities)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    private async Task<Pokemon?> GetByName(string name)
    {
        name = name.ToLower();
        return await _context.Pokemons
            .Include(i => i.Types)
            .Include(i => i.Moves)
            .Include(i => i.Abilities)
            .FirstOrDefaultAsync(i => i.Name.Equals(name));
    }
}