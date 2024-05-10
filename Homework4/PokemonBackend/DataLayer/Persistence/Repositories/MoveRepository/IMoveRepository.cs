using Domain.Entities;

namespace DataLayer.Persistence.Repositories.MoveRepository;

public interface IMoveRepository
{
    Task AddAsync(Move move);
    Task RemoveAsync(Move move);
    Task UpdateAsync(Move move);
    Task<List<Move>> GetAllAsync();
    Task<Move?> GetByIdAsync(int id);
}