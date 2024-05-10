using DataLayer.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Persistence.Repositories.PokemonRepository;

public class PokemonRepository : IPokemonRepository
{
    private readonly AppDbContext _context;

    public PokemonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Pokemon pokemon)
    {
        await _context.AddAsync(pokemon);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Pokemon pokemon)
    {
        _context.Remove(pokemon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pokemon pokemon)
    {
        _context.Update(pokemon);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pokemon>> GetAllAsync(int limit, int offset)
    {
        return await _context.Pokemons
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Pokemon?> GetByIdAsync(int id)
    {
        return await _context.Pokemons
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Pokemon?> GetByNameAsync(string name)
    {
        return await _context.Pokemons
            .FirstOrDefaultAsync(i => i.Name.Equals(name));
    }

    public async Task<List<Pokemon>> FilterByNameAsync(string filter)
    {
        return await _context.Pokemons
            .Where(i => i.Name.Contains(filter))
            .ToListAsync();
    }
}