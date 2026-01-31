using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivateAccountService(IAccountRepository pAccountRepository) : DeactivateEntityService<Account, IAccountRepository>(pAccountRepository)
{
}
