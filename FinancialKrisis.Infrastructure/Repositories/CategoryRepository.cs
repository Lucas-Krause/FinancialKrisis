using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class CategoryRepository(FinancialKrisisDbContext pContext) : ICategoryRepository
{
    public async Task AddAsync(Category pCategory)
    {
        pContext.Categories.Add(pCategory);
        await pContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category pCategory)
    {
        pContext.Categories.Update(pCategory);
        await pContext.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid pId)
    {
        return await pContext.Categories.FindAsync(pId);
    }

    public async Task<Category> GetByIdOrThrowAsync(Guid pId)
    {
        return await pContext.Categories.FindAsync(pId) ?? throw new InvalidOperationException("Category not found.");
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync()
    {
        return await pContext.Categories.AsNoTracking().ToListAsync();
    }
}
