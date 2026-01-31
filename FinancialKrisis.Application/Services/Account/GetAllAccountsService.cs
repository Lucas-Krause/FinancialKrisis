using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllAccountsService(IAccountRepository pAccountRepository) : GetAllEntitiesService<Account, IAccountRepository>(pAccountRepository)
{
}
