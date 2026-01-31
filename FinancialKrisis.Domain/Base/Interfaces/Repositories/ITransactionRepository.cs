using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Interfaces;

public interface ITransactionRepository : IFinancialMovementRepository<Transaction>
{
}
