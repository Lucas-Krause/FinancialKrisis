using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetTransactionByIdService(ITransactionRepository pTransactionRepository) : GetEntityEntityByIdService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
