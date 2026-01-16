using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateAccountService(IAccountRepository pRepository)
{
    public async Task<Account> ExecuteAsync(UpdateAccountDTO pUpdateAccountDTO)
    {
        Account account = await pRepository.GetByIdOrThrowAsync(pUpdateAccountDTO.Id);
        account.Rename(pUpdateAccountDTO.Name);
        account.ChangeAccountNumber(pUpdateAccountDTO.AccountNumber);
        await pRepository.UpdateAsync(account);
        return account;
    }
}
