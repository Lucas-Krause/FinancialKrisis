using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreateAccountService(IAccountRepository pAccountRepository, IUnitOfWork pUnitOfWork) : CreateEntityService<Account, IAccountRepository, CreateAccountDTO>(pAccountRepository, pUnitOfWork)
{
    protected override async Task<Account> CreateEntity(CreateAccountDTO pCreateDTO)
    {
        var account = new Account(pCreateDTO.Name, pCreateDTO.AccountNumber, pCreateDTO.InitialBalance);

        if (await EntityRepository.ExistsByNameAsync(account.Name))
            throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);

        return account;
    }
}
