using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAccountByIdService(IAccountRepository pRepository)
{
    public async Task<Account?> ExecuteAsync(Guid pAccountId)
    {
        try
        {
            return await pRepository.GetByIdAsync(pAccountId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
