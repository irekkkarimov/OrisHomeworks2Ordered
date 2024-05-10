using Domain.Entities;

namespace DataLayer.Persistence.Repositories.AbilityRepository;

public interface IAbilityRepository
{
    Task AddAsync(Ability ability);
    Task RemoveAsync(Ability ability);
    Task UpdateAsync(Ability ability);
    Task<List<Ability>> GetAllAsync();
    Task<Ability?> GetByIdAsync(int id);
}