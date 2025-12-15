using Microsoft.EntityFrameworkCore.Storage;

namespace LoyaltyPointSystem.Shared.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}