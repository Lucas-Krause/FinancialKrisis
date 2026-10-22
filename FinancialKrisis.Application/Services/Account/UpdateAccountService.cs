using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class UpdateAccountService(IAccountRepository pAccountRepository) : UpdateEntityService<Account, IAccountRepository, UpdateAccountDTO>(pAccountRepository)
{
    protected override async Task ApplyChangesToEntity(Account pAccount, UpdateAccountDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
        {
            pAccount.ChangeName(pUpdateDTO.Name.Value!);

            if (await EntityRepository.ExistsByNameAsync(pAccount.Name, pAccount.Id))
                throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);
        }

        if (pUpdateDTO.AccountNumber.IsDefined)
            pAccount.ChangeAccountNumber(pUpdateDTO.AccountNumber.Value!);

        if (pUpdateDTO.InitialBalance.IsDefined)
            pAccount.ChangeInitialBalance(pUpdateDTO.InitialBalance.Value);
    }
}
