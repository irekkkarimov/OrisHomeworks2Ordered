using DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Entities.Type;

namespace DataLayer.Persistence.Repositories.TypeRepository;

public class TypeRepository : ITypeRepository
{
    private readonly AppDbContext _context;

    public TypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Type type)
    {
        await _context.AddAsync(type);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Type type)
    {
        _context.Remove(type);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Type type)
    {
        _context.Update(type);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Type>> GetAllAsync()
    {
        return await _context.Types.ToListAsync();
    }

    public async Task<Type?> GetByIdAsync(int id)
    {
        return await _context.Types
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}