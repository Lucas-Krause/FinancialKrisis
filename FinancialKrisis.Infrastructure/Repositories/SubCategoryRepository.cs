using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class SubCategoryRepository(FinancialKrisisDbContext pContext) : BaseRepository<SubCategory>(pContext), ISubCategoryRepository
{
    public override async Task<SubCategory?> GetByIdAsync(Guid id)
    {
        return await _dbSet.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public override async Task<IReadOnlyList<SubCategory>> GetAllAsync()
    {
        return await _dbSet.Include(sc => sc.Category).AsNoTracking().ToListAsync();
    }
}
