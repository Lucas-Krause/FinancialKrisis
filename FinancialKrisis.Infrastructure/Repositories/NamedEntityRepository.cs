using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Domain.ValueObjects;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class NamedEntityRepository<TEntity>(FinancialKrisisDbContext pContext) : BaseRepository<TEntity>(pContext)
    where TEntity : class, INamedEntity
{
    public async Task<bool> ExistsByNameAsync(EntityName pName, Guid? pIgnoredId = null)
    {
        return await _dbSet.AnyAsync(e => e.Name.NormalizedValue == pName.NormalizedValue && (pIgnoredId == null || e.Id != pIgnoredId.Value));
    }
}
