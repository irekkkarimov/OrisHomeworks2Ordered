using Microsoft.EntityFrameworkCore;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Persistence.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Game> Entities => _context.Games;
    
    public DbContext Context => _context;
    public Task<Game> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _context.Games
            .Include(i => i.Platforms)
            .Include(i => i.Categories)
            .Include(i => i.Companies)
            .Include(i => i.Images)
            .ToListAsync();
    }

    public Task<Game> AddAsync(Game entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Game entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Game entity)
    {
        throw new NotImplementedException();
    }
}