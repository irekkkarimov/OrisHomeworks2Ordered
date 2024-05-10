using Domain.Entities;

namespace DataLayer.Persistence.Repositories.PokemonRepository;

public interface IPokemonRepository
{
    Task AddAsync(Pokemon pokemon);
    Task RemoveAsync(Pokemon pokemon);
    Task UpdateAsync(Pokemon pokemon);
    Task<List<Pokemon>> GetAllAsync(int limit, int offset);
    Task<Pokemon?> GetByIdAsync(int id);
    Task<Pokemon?> GetByNameAsync(string name);
    Task<List<Pokemon>> FilterByNameAsync(string filter);
}