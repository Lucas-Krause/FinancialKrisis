using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Domain.Repositories;

public interface IPlannedTransactionRepository : IFinancialMovementRepository<PlannedTransaction>
{
}
