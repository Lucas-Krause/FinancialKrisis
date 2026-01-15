using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllAccountsService(IAccountRepository pRepository)
{
    public async Task<IReadOnlyList<Account>> ExecuteAsync()
    {
        return await pRepository.GetAllAsync();
    }
}
