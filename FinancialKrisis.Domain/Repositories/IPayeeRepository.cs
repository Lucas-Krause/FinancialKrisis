using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface IPayeeRepository
{
    Task AddAsync(Payee pPayee);
    Task UpdateAsync(Payee pPayee);
    Task<Payee?> GetByIdAsync(Guid pId);
    Task<Payee> GetByIdOrThrowAsync(Guid pId);
    Task<IReadOnlyList<Payee>> GetAllAsync();
}
