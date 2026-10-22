using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class AccountScenario : Scenario<AccountScenario, CreateAccountDTO, UpdateAccountDTO, Account>
{
    public AccountScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Name = $"Test Account {Guid.NewGuid():N}";
        CreateInput.AccountNumber = "1234567";

        CreateFunc = Context.CreateAccountService.ExecuteAsync;
        UpdateFunc = Context.UpdateAccountService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateAccountService.ExecuteAsync;
        ReactivateFunc = Context.ReactivateAccountService.ExecuteAsync;
    }

    public AccountScenario AsCurrentAccount()
    {
        return AsCurrent();
    }
}

