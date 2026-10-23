using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class BaseRepository<TEntity>(FinancialKrisisDbContext pContext) where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet = pContext.Set<TEntity>();

    public virtual void Add(TEntity pEntity)
    {
        _dbSet.Add(pEntity);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid pId)
    {
        return await _dbSet.FindAsync(pId);
    }

    public virtual async Task<TEntity> GetByIdOrThrowAsync(Guid pId)
    {
        return await GetByIdAsync(pId) ?? throw new DomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(TEntity));
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual void Remove(TEntity pEntity)
    {
        _dbSet.Remove(pEntity);
    }
}
