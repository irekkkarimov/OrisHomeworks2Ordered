using Microsoft.EntityFrameworkCore;
using TeamHost.Domain.Common.Interfaces;

namespace TeamHost.Application.Interfaces.Repositories;

public interface IGenericRepository<T>
where T : class, IEntity
{
    IQueryable<T> Entities { get; }
    DbContext Context { get; }

    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}