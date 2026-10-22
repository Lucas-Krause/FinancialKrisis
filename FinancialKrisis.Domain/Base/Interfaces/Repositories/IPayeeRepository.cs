using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Interfaces;

public interface IPayeeRepository : IGenericRepository<Payee>
{
    Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null);
}
