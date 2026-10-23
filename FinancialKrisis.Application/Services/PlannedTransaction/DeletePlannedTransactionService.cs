using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeletePlannedTransactionService(IPlannedTransactionRepository pPlannedTransactionRepository, IUnitOfWork pUnitOfWork) : DeleteEntityService<PlannedTransaction, IPlannedTransactionRepository>(pPlannedTransactionRepository, pUnitOfWork)
{
}
