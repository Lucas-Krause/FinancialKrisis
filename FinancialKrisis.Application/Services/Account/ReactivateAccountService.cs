using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateAccountService(IAccountRepository pAccountRepository) : ReactivateEntityService<Account, IAccountRepository>(pAccountRepository)
{
}
