using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class SubCategoryRepository(FinancialKrisisDbContext pContext) : ISubCategoryRepository
{
    public async Task AddAsync(SubCategory subCategory)
    {
        pContext.SubCategories.Add(subCategory);
        await pContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(SubCategory subCategory)
    {
        pContext.SubCategories.Update(subCategory);
        await pContext.SaveChangesAsync();
    }

    public async Task<SubCategory?> GetByIdAsync(Guid id)
    {
        return await pContext.SubCategories.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public async Task<SubCategory> GetByIdOrThrowAsync(Guid id)
    {
        return await pContext.SubCategories.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id)
            ?? throw new InvalidOperationException("SubCategory not found.");
    }

    public async Task<IReadOnlyList<SubCategory>> GetAllAsync()
    {
        return await pContext.SubCategories.Include(sc => sc.Category).AsNoTracking().ToListAsync();
    }
}
