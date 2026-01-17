using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ISubcategoryRepository
{
    Task AddAsync(Subcategory subcategory);
    Task UpdateAsync(Subcategory subcategory);
    Task<Subcategory?> GetByIdAsync(Guid id);
    Task<Subcategory> GetByIdOrThrowAsync(Guid id);
    Task<IReadOnlyList<Subcategory>> GetAllAsync();
}
