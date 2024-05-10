using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonAPI.Models;
using PokemonAPI.Services.PokeApiService;

namespace PokemonAPI.Controllers;

[Route("[controller]/[action]")]
public class PokemonController : ControllerBase
{
    private readonly IPokeApiService _pokeApiService;

    public PokemonController(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int limit, int offset)
    {
        var pokemonResponse = await _pokeApiService.GetByFilterAsync("", limit, offset);
        return Ok(pokemonResponse);
    }

    [HttpGet]
    [Route("{filter}")]
    public async Task<IActionResult> GetByFilter(int limit, int offset, string filter)
    {
        var pokemonResponse = await _pokeApiService.GetByFilterAsync(filter, limit, offset);
        return Ok(pokemonResponse);
    }

    [HttpGet]
    [Route("{idOrName}")]
    public async Task<IActionResult> GetByIdOrName(string idOrName)
    {
        var pokemonDataDto = await _pokeApiService.GetByIdOrNameAsync(idOrName);

        if (pokemonDataDto is null)
            return NotFound();
 
        return Ok(new { results = pokemonDataDto });
    }
}