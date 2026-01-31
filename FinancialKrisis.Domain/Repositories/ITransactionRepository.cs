using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface ITransactionRepository : IFinancialMovementRepository<Transaction>
{
}
