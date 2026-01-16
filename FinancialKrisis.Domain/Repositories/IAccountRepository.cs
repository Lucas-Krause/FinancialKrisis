using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account pAccount);
    Task UpdateAsync(Account pAccount);
    Task<Account?> GetByIdAsync(Guid pId);
    Task<Account> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<Account>> GetAllAsync();
}
