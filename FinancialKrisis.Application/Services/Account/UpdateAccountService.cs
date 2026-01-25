using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateAccountService(IAccountRepository pRepository)
{
    public async Task<Account> ExecuteAsync(UpdateAccountDTO pUpdateAccountDTO)
    {
        try
        {
            var account = (Account)ActiveEntityValidator.EnsureIsActive(await pRepository.GetByIdOrThrowAsync(pUpdateAccountDTO.Id));

            if (pUpdateAccountDTO.Name.IsDefined)
                account.Rename(pUpdateAccountDTO.Name.Value!);

            if (pUpdateAccountDTO.AccountNumber.IsDefined)
                account.ChangeAccountNumber(pUpdateAccountDTO.AccountNumber.Value!);

            if (pUpdateAccountDTO.InitialBalance.IsDefined)
                account.ChangeInitialBalance(pUpdateAccountDTO.InitialBalance.Value);

            await pRepository.UpdateAsync(account);
            return account;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
