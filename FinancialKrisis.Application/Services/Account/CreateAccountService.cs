using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateAccountService(IAccountRepository pRepository)
{
    public async Task<Account> ExecuteAsync(CreateAccountDTO pCreateAccountDTO)
    {
        try
        {
            var account = new Account(pCreateAccountDTO.Name, pCreateAccountDTO.AccountNumber, pCreateAccountDTO.InitialBalance);
            await pRepository.AddAsync(account);
            return account;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
