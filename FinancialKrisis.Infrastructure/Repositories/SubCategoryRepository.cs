using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
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
}
