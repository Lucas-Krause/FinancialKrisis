using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ISubCategoryRepository
{
    Task AddAsync(SubCategory subCategory);
    Task UpdateAsync(SubCategory subCategory);
    Task<SubCategory?> GetByIdAsync(Guid id);
    Task<SubCategory> GetByIdOrThrowAsync(Guid id);
    Task<IReadOnlyList<SubCategory>> GetAllAsync();
}
