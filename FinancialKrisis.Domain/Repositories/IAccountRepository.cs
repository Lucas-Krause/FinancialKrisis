using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account pAccount);
    Task UpdateAsync(Account pAccount);
    Task DeleteAsync(Guid pId);
    Task<Account?> GetByIdAsync(Guid pId);
    Task<IReadOnlyList<Account>> GetAllAsync();
}
