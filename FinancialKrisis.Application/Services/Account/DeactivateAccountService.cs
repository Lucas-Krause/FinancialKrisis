using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateAccountService(IAccountRepository pAccountRepository) : DeactivateEntityService<Account, IAccountRepository>(pAccountRepository)
{
}
