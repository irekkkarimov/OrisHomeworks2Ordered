using AutoMapper;
using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO.Move;

namespace PokemonAPI.Controllers;

[Route("[controller]/[action]")]
public class MoveController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MoveController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var move = await _context.Moves
            .Include(i => i.Type)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (move is null)
            return NotFound("Move not found");

        return Ok(_mapper.Map<MoveGetDto>(move));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int limit = 1000000, [FromQuery] int offset = 0)
    {
        var moves = await _context.Moves
            .Skip(offset)
            .Take(limit)
            .Include(i => i.Type)
            .ToListAsync();

        return Ok(moves.Select(i => _mapper.Map<MoveGetDto>(i)));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MoveAddDto moveAddDto)
    {
        moveAddDto.Name = moveAddDto.Name.ToLower();
        var doesMoveWithThisNameExist = await _context.Moves
            .AnyAsync(i => i.Name.Equals(moveAddDto.Name));

        if (doesMoveWithThisNameExist)
            return BadRequest("Move with this name already exists");
        
        var typeFromDb = await _context.Types
            .FirstOrDefaultAsync(i => i.Id == moveAddDto.TypeId);

        if (typeFromDb is null)
            return BadRequest("Type not found");
        
        var newMove = _mapper.Map<Move>(moveAddDto);
        await _context.AddAsync(newMove);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<MoveGetAfterAddingDto>(newMove));
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] MoveUpdateDto moveUpdateDto)
    {
        var moveFromDb = await _context.Moves
            .FirstOrDefaultAsync(i => i.Id == moveUpdateDto.Id);

        if (moveFromDb is null)
            return NotFound("Move not found");

        if (moveUpdateDto.TypeId != 0)
        {
            var typeFromDb = await _context.Types
                .FirstOrDefaultAsync(i => i.Id == moveUpdateDto.TypeId);

            if (typeFromDb is null)
                return BadRequest("Type not found");

            moveFromDb.Type = typeFromDb;
        }

        moveFromDb.Name = moveUpdateDto.Name;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] MoveDeleteDto moveDeleteDto)
    {
        var moveFromDb = await _context.Moves
            .FirstOrDefaultAsync(i => i.Id == moveDeleteDto.Id);

        if (moveFromDb is null)
            return NotFound("Move not found");

        _context.Moves.Remove(moveFromDb);
        await _context.SaveChangesAsync();
        return Ok();
    }
}