using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAccountByIdService(IAccountRepository pRepository)
{
    public async Task<Account?> ExecuteAsync(Guid pAccountId)
    {
        return await pRepository.GetByIdAsync(pAccountId);
    }
}
