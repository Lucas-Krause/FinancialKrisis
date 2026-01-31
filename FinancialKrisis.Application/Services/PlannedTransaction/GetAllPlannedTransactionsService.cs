using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllPlannedTransactionsService(IPlannedTransactionRepository pPlannedTransactionRepository) : GetAllEntitiesService<PlannedTransaction, IPlannedTransactionRepository>(pPlannedTransactionRepository)
{
}

