using System.Collections;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Common;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Hashtable _repositories = new();
    private bool disposed = false;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {
        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type]!;
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
    {
        throw new NotImplementedException();
    }

    public Task Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) 
            return;
        
        if (disposing)
            _context.Dispose();

        disposed = true;
    }
}