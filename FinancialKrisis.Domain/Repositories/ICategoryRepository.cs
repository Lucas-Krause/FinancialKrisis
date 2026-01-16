using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category pCategory);
    Task UpdateAsync(Category pCategory);
    Task<Category?> GetByIdAsync(Guid pId);
    Task<Category> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<Category>> GetAllAsync();
}
