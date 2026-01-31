using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeletePlannedTransactionService(IPlannedTransactionRepository pPlannedTransactionRepository) : DeleteEntityService<PlannedTransaction, IPlannedTransactionRepository>(pPlannedTransactionRepository)
{
}
