namespace DataLayer.Persistence.Repositories.TypeRepository;
using Type = Domain.Entities.Type;

public interface ITypeRepository
{
    Task AddAsync(Type type);
    Task RemoveAsync(Type type);
    Task UpdateAsync(Type type);
    Task<List<Type>> GetAllAsync();
    Task<Type?> GetByIdAsync(int id);
}