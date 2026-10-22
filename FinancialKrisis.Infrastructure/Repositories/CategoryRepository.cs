using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class CategoryRepository(FinancialKrisisDbContext pContext) : BaseRepository<Category>(pContext), ICategoryRepository
{
    public override async Task<Category?> GetByIdAsync(Guid pId)
    {
        return await _dbSet.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == pId);
    }

    public async Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null)
    {
        string normalizedName = pName.Trim().ToLower();
        return await _dbSet.AnyAsync(c => c.Name.ToLower() == normalizedName && (pIgnoredId == null || c.Id != pIgnoredId.Value));
    }
}
