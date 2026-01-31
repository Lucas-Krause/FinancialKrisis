using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class TransactionRepository(FinancialKrisisDbContext pContext) : BaseRepository<Transaction>(pContext), ITransactionRepository
{
}
