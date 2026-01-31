using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<IReadOnlyList<Subcategory>> GetByCategoryIdAsync(Guid pCategoryId);
}
