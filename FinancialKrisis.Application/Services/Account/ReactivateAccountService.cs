using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateAccountService(IAccountRepository pAccountRepository, IUnitOfWork pUnitOfWork) : ReactivateEntityService<Account, IAccountRepository>(pAccountRepository, pUnitOfWork)
{
}
