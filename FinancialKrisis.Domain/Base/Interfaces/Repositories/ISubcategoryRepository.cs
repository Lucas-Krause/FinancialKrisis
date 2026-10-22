using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Interfaces;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<IReadOnlyList<Subcategory>> GetByCategoryIdAsync(Guid pCategoryId);
    Task<bool> ExistsByNameAsync(Guid pCategoryId, string pName, Guid? pIgnoredId = null);
}
