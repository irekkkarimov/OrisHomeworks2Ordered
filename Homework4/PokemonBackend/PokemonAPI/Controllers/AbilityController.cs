using AutoMapper;
using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO.Ability;

namespace PokemonAPI.Controllers;

[Route("[controller]/[action]")]
public class AbilityController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AbilityController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var ability = await _context.Abilities
            .FirstOrDefaultAsync(i => i.Id == id);

        if (ability is null)
            return NotFound("Ability not found");

        return Ok(_mapper.Map<AbilityGetDto>(ability));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int limit = 1000000, [FromQuery] int offset = 0)
    {
        var abilities = await _context.Abilities
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return Ok(abilities.Select(i => _mapper.Map<AbilityGetDto>(i)));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AbilityAddDto abilityAddDto)
    {
        abilityAddDto.Name = abilityAddDto.Name.ToLower();

        var doesAbilityWithThisNameExist = await _context.Abilities
            .AnyAsync(i => i.Name.Equals(abilityAddDto.Name));

        if (doesAbilityWithThisNameExist)
            return BadRequest("Ability with this name already exists");
        
        var newAbility = _mapper.Map<Ability>(abilityAddDto);
        await _context.AddAsync(newAbility);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<AbilityGetAfterAddingDto>(newAbility));
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] AbilityUpdateDto abilityUpdateDto)
    {
        var abilityFromDb = await _context.Abilities
            .FirstOrDefaultAsync(i => i.Id == abilityUpdateDto.Id);

        if (abilityFromDb is null)
            return NotFound("Ability not found");

        abilityFromDb.Name = abilityUpdateDto.Name;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] AbilityDeleteDto abilityDeleteDto)
    {
        var abilityFromDb = await _context.Abilities
            .FirstOrDefaultAsync(i => i.Id == abilityDeleteDto.Id);

        if (abilityFromDb is null)
            return NotFound("Ability not found");

        _context.Abilities.Remove(abilityFromDb);
        await _context.SaveChangesAsync();
        return Ok();
    }
}