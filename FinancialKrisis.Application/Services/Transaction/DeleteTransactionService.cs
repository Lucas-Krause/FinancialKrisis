using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeleteTransactionService(ITransactionRepository pTransactionRepository) : DeleteEntityService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
