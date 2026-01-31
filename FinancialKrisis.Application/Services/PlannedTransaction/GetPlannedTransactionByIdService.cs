using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetPlannedTransactionByIdService(IPlannedTransactionRepository pPlannedTransactionRepository) : GetEntityEntityByIdService<PlannedTransaction, IPlannedTransactionRepository>(pPlannedTransactionRepository)
{
}
