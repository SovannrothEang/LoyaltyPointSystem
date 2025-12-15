using System.Collections.Concurrent;
using LoyaltyPointSystem.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoyaltyPointSystem.Shared.Interfaces;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ConcurrentDictionary<Type, object> _repositories = [];
    private bool _disposed;
    
    public async Task<int> SaveChangesAsync(CancellationToken  cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return  _dbContext.Database.BeginTransactionAsync();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, () =>
            new GenericRepository<TEntity>(_dbContext));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed &&  disposing)
        {
            _dbContext.Dispose();
            _disposed = true;
        }
    }
}