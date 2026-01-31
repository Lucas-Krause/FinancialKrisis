using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeleteTransactionService(ITransactionRepository pTransactionRepository) : DeleteEntityService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
