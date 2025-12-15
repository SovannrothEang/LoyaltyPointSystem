using System.Linq.Expressions;
using LoyaltyPointSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyPointSystem.Shared.Interfaces;

public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly DbSet<TEntity> _dbSet =  dbContext.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[]? includes)
    {
        IQueryable<TEntity> queryable = _dbSet;
        if (filter != null)
            queryable = queryable.Where(filter);
        if (includes is { Length: > 0 })
        {
            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }
            // queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));
        }
        if (orderBy != null)
            queryable = orderBy(queryable);
        
        return  await queryable
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public async Task<TEntity?> GetFirstOrDefault(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    
    public async Task CreateBatchAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Update(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}