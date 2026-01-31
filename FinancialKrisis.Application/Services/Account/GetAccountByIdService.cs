using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAccountByIdService(IAccountRepository pAccountRepository) : GetEntityEntityByIdService<Account, IAccountRepository>(pAccountRepository)
{
}
