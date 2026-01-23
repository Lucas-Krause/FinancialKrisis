using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class CategoryRepository(FinancialKrisisDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
    public override async Task<Category?> GetByIdAsync(Guid pId)
    {
        return await _dbSet.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == pId);
    }
}
