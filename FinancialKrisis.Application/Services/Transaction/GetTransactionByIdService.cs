using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetTransactionByIdService(ITransactionRepository pTransactionRepository) : GetEntityEntityByIdService<Transaction, ITransactionRepository>(pTransactionRepository)
{
}
