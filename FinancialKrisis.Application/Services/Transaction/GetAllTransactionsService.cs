using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllTransactionsService(ITransactionRepository pTransactionRepository) : GetAllEntitiesService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
