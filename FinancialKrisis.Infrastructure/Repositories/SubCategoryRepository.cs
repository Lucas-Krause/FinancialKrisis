using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class SubcategoryRepository(FinancialKrisisDbContext pContext) : BaseRepository<Subcategory>(pContext), ISubcategoryRepository
{
    public override async Task<Subcategory?> GetByIdAsync(Guid id)
    {
        return await _dbSet.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public override async Task<IReadOnlyList<Subcategory>> GetAllAsync()
    {
        return await _dbSet.Include(sc => sc.Category).AsNoTracking().ToListAsync();
    }
}
