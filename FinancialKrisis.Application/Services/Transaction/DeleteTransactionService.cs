using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeleteTransactionService(ITransactionRepository pTransactionRepository, IUnitOfWork pUnitOfWork) : DeleteEntityService<Transaction, ITransactionRepository>(pTransactionRepository, pUnitOfWork)
{
}
