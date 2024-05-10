using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Persistence.Repositories.AbilityRepository;

public class AbilityRepository : IAbilityRepository
{
    private readonly AppDbContext _context;

    public AbilityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ability ability)
    {
        await _context.AddAsync(ability);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Ability ability)
    {
        _context.Remove(ability);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ability ability)
    {
        _context.Update(ability);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Ability>> GetAllAsync()
    {
        return await _context.Abilities.ToListAsync();
    }

    public async Task<Ability?> GetByIdAsync(int id)
    {
        return await _context.Abilities.FirstOrDefaultAsync(i => i.Id == id);
    }
}