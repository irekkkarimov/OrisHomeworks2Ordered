using AutoMapper;
using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO.Pokemon;
using PokemonAPI.DTO.Pokemon.Relationships;
using PokemonAPI.Services.PokemonService;

namespace PokemonAPI.Controllers;

[Route("[controller]/[action]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PokemonController(IPokemonService service, AppDbContext context, IMapper mapper)
    {
        _pokemonService = service;
        _context = context;
        _mapper = mapper;
    }
 
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int limit, [FromQuery] int offset)
    {
        var responseDto = await _pokemonService.GetAllAsync(limit, offset);
        return Ok(responseDto);
    }

    [HttpGet]
    [Route("{filter}")]
    public async Task<IActionResult> GetByFilter(int limit, int offset, string filter)
    {
        var responseDto = await _pokemonService.GetByFilterAsync(filter, limit, offset);
        return Ok(responseDto);
    }

    [HttpGet]
    [Route("{idOrName}")]
    public async Task<IActionResult> GetByIdOrName(string idOrName)
    {
        var pokemonDataDto = await _pokemonService.GetByIdOrNameAsync(idOrName);
        return Ok(pokemonDataDto);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PokemonAddDto addDto)
    {
        addDto.Name = addDto.Name.ToLower();
        var pokemonWithSameName = await _context.Pokemons
            .AnyAsync(i => i.Name.ToLower().Equals(addDto.Name.ToLower()));

        if (pokemonWithSameName)
            return BadRequest("Pokemon with this name already exists");
        
        var newPokemon = _mapper.Map<Pokemon>(addDto);
        await _context.AddAsync(newPokemon);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<PokemonGetAfterAddingDto>(newPokemon));
    }

    [HttpPatch]
    public async Task<IActionResult> AddType([FromBody] PokemonAddTypeDto addTypeDto)
    {
        var pokemon = await _context.Pokemons.Include(pokemon => pokemon.Types)
            .FirstOrDefaultAsync(i => i.Id == addTypeDto.PokemonId);

        if (pokemon is null)
            return BadRequest("Pokemon not found");

        var pokemonTypeRelationship = pokemon.Types
            .FirstOrDefault(i => i.Id == addTypeDto.TypeId);

        if (pokemonTypeRelationship is not null)
            return BadRequest("Pokemon already has this type");

        var type = await _context.Types
            .FirstOrDefaultAsync(i => i.Id == addTypeDto.TypeId);

        if (type is null)
            return BadRequest("Type not found");
        
        pokemon.Types.Add(type);
        await _context.SaveChangesAsync();
        return Ok();
    }
}