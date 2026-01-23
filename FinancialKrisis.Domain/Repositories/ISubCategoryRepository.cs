using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ISubcategoryRepository
{
    Task AddAsync(Subcategory subcategory);
    Task UpdateAsync(Subcategory subcategory);
    Task<IReadOnlyList<Subcategory>> GetByCategoryIdAsync(Guid pCategoryId);
    Task<Subcategory?> GetByIdAsync(Guid pSubcategoryId);
    Task<Subcategory> GetByIdOrThrowAsync(Guid pSubcategoryId);
    Task<IReadOnlyList<Subcategory>> GetAllAsync();
}
