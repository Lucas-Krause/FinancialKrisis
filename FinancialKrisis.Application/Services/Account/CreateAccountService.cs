using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateAccountService(IAccountRepository pAccountRepository) : CreateEntityService<Account, IAccountRepository, CreateAccountDTO>(pAccountRepository)
{
    protected override async Task<Account> CreateEntity(CreateAccountDTO pCreateDTO)
    {
        return new Account(pCreateDTO.Name, pCreateDTO.AccountNumber, pCreateDTO.InitialBalance);
    }
}
