using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Interfaces;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<IReadOnlyList<Subcategory>> GetByCategoryIdAsync(Guid pCategoryId);
    Task<bool> ExistsByNameAsync(Guid pCategoryId, EntityName pName, Guid? pIgnoredId = null);
}
