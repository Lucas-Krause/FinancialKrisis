using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllPlannedTransactionsService(IPlannedTransactionRepository pPlannedTransactionRepository) : GetAllEntitiesService<PlannedTransaction, IPlannedTransactionRepository>(pPlannedTransactionRepository)
{
}

