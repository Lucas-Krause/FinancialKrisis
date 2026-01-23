using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Infrastructure.Errors;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FinancialKrisis.Infrastructure.Repositories;

public class BaseRepository<TEntity>(FinancialKrisisDbContext pContext) where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet = pContext.Set<TEntity>();

    public virtual async Task AddAsync(TEntity pEntity)
    {
        _dbSet.Add(pEntity);
        await pContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity pEntity)
    {
        _dbSet.Update(pEntity);
        await pContext.SaveChangesAsync();
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

    public virtual async Task DeleteAsync(Guid pId)
    {
        TEntity? entity = await GetByIdAsync(pId);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await pContext.SaveChangesAsync();
        }
    }
}
