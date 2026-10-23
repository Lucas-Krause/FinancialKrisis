using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Domain.ValueObjects;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class SubcategoryRepository(FinancialKrisisDbContext pContext) : BaseRepository<Subcategory>(pContext), ISubcategoryRepository
{
    public async Task<IReadOnlyList<Subcategory>> GetByCategoryIdAsync(Guid pCategoryId)
    {
        return await _dbSet.Where(sc => sc.CategoryId == pCategoryId)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<bool> ExistsByNameAsync(Guid pCategoryId, EntityName pName, Guid? pIgnoredId = null)
    {
        return await _dbSet.AnyAsync(sc => sc.CategoryId == pCategoryId && sc.Name.NormalizedValue == pName.NormalizedValue && (pIgnoredId == null || sc.Id != pIgnoredId.Value));
    }
}
