using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class CategoryRepository(FinancialKrisisDbContext pContext) : NamedEntityRepository<Category>(pContext), ICategoryRepository
{
    public override async Task<Category?> GetByIdAsync(Guid pId)
    {
        return await _dbSet.Include(c => c.Subcategories).FirstOrDefaultAsync(c => c.Id == pId);
    }
}
