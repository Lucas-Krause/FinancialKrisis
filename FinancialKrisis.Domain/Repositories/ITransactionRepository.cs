using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction pTransaction);
    Task UpdateAsync(Transaction pTransaction);
    Task DeleteAsync(Guid pId);
    Task<Transaction?> GetByIdAsync(Guid pId);
    Task<Transaction> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<Transaction>> GetAllAsync();
}
