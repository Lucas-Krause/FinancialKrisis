using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateAccountService(IAccountRepository pRepository)
{
    public async Task ExecuteAsync(Guid pAccountId)
    {
        try
        {
            Account account = await pRepository.GetByIdOrThrowAsync(pAccountId);
            account.Deactivate();
            await pRepository.UpdateAsync(account);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
