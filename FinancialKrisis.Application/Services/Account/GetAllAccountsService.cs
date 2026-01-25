using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllAccountsService(IAccountRepository pRepository)
{
    public async Task<IReadOnlyList<Account>> ExecuteAsync()
    {
        try
        {
            return await pRepository.GetAllAsync();
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
