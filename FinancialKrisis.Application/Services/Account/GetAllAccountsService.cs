using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllAccountsService(IAccountRepository pAccountRepository) : GetAllEntitiesService<Account, IAccountRepository>(pAccountRepository)
{
}
