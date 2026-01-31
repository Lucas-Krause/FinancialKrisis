using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class AccountRepository(FinancialKrisisDbContext context) : BaseRepository<Account>(context), IAccountRepository
{
}
