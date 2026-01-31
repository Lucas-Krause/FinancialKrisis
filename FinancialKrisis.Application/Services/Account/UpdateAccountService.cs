using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateAccountService(IAccountRepository pAccountRepository) : UpdateEntityService<Account, IAccountRepository, UpdateAccountDTO>(pAccountRepository)
{
    protected override async Task ApplyChangesToEntity(Account pAccount, UpdateAccountDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
            pAccount.ChangeName(pUpdateDTO.Name.Value!);

        if (pUpdateDTO.AccountNumber.IsDefined)
            pAccount.ChangeAccountNumber(pUpdateDTO.AccountNumber.Value!);

        if (pUpdateDTO.InitialBalance.IsDefined)
            pAccount.ChangeInitialBalance(pUpdateDTO.InitialBalance.Value);
    }
}
