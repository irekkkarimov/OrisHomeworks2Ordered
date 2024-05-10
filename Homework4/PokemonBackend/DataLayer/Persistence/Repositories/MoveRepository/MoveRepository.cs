using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Persistence.Repositories.MoveRepository;

public class MoveRepository : IMoveRepository
{
    private readonly AppDbContext _context;

    public MoveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Move move)
    {
        await _context.AddAsync(move);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Move move)
    {
        _context.Remove(move);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Move move)
    {
        _context.Update(move);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Move>> GetAllAsync()
    {
        return await _context.Moves.ToListAsync();
    }

    public async Task<Move?> GetByIdAsync(int id)
    {
        return await _context.Moves
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}