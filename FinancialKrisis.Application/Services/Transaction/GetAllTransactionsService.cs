using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllTransactionsService(ITransactionRepository pTransactionRepository) : GetAllEntitiesService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
