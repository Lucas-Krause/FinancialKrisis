using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null);
}
