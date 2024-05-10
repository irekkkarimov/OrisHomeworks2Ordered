using AutoMapper;
using DataLayer.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO.Type;
using Type = Domain.Entities.Type;

namespace PokemonAPI.Controllers;

[Route("[controller]/[action]")]
public class TypeController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TypeController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var type = await _context.Types
            .FirstOrDefaultAsync(i => i.Id == id);
        if (type is null)
            return NotFound("Type not found");

        return Ok(_mapper.Map<TypeGetDto>(type));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int limit = 1000000, [FromQuery] int offset = 0)
    {
        var types = await _context.Types
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return Ok(types.Select(i => _mapper.Map<TypeGetDto>(i)));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TypeAddDto typeAddDto)
    {
        typeAddDto.Name = typeAddDto.Name.ToLower();

        var doesTypeWithThisNameExist = await _context.Types
            .AnyAsync(i => i.Name.Equals(typeAddDto.Name));

        if (doesTypeWithThisNameExist)
            return BadRequest("Type with this name already exists");
        
        var newType = _mapper.Map<Type>(typeAddDto);
        await _context.AddAsync(newType);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<TypeGetAfterAddingDto>(newType));
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] TypeUpdateDto typeUpdateDto)
    {
        typeUpdateDto.Name = typeUpdateDto.Name.ToLower();
        
        var typeFromDb = await _context.Types
            .FirstOrDefaultAsync(i => i.Id == typeUpdateDto.Id);

        if (typeFromDb is null)
            return NotFound("Type not found");

        typeFromDb.Name = typeUpdateDto.Name;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] TypeDeleteDto typeDeleteDto)
    {
        var typeFromDb = await _context.Types
            .FirstOrDefaultAsync(i => i.Id == typeDeleteDto.Id);

        if (typeFromDb is null)
            return NotFound("Type not found");

        _context.Remove(typeFromDb);
        await _context.SaveChangesAsync();
        return Ok();
    }
}