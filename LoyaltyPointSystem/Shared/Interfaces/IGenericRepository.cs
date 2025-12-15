using System.Linq.Expressions;

namespace LoyaltyPointSystem.Shared.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[]? includes);
    Task<TEntity?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken = default);
    Task<TEntity?> GetFirstOrDefault(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
    Task CreateBatchAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
    void UpdateAsync(
        TEntity entity);
    void DeleteAsync(
        TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}