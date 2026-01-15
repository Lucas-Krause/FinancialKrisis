using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Application.DTOs;

namespace FinancialKrisis.Application.Services;

public class CreateAccountService(IAccountRepository pRepository)
{
    public async Task<Account> ExecuteAsync(CreateAccountDTO pCreateAccountDto)
    {
        var account = new Account(pCreateAccountDto.Name, pCreateAccountDto.InitialBalance);
        await pRepository.AddAsync(account);
        return account;
    }
}
