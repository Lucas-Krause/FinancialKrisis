using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class AccountRepository(FinancialKrisisDbContext pContext) : BaseRepository<Account>(pContext), IAccountRepository
{
}
