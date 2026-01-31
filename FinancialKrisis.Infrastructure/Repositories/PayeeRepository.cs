using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class PayeeRepository(FinancialKrisisDbContext context) : BaseRepository<Payee>(context), IPayeeRepository
{
}
