using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null);
}
