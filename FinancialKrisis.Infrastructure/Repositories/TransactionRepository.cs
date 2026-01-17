using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class TransactionRepository(FinancialKrisisDbContext pContext) : BaseRepository<Transaction>(pContext), ITransactionRepository
{
}
