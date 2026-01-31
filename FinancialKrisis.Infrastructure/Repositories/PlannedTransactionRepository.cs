using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class PlannedTransactionRepository(FinancialKrisisDbContext pContext) : BaseRepository<PlannedTransaction>(pContext), IPlannedTransactionRepository
{
}
