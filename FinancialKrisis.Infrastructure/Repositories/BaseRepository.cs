using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class BaseRepository<TEntity>(FinancialKrisisDbContext pContext) where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet = pContext.Set<TEntity>();

    public virtual async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await pContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await pContext.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<TEntity> GetByIdOrThrowAsync(Guid id)
    {
        return await GetByIdAsync(id) ?? throw new InvalidOperationException($"{typeof(TEntity).Name} not found.");
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        TEntity? entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await pContext.SaveChangesAsync();
        }
    }
}
