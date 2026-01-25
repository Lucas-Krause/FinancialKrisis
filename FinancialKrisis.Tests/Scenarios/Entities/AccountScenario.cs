using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class AccountScenario : Scenario<AccountScenario, CreateAccountDTO, UpdateAccountDTO, Account>
{
    public AccountScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Name = "Test Account";
        CreateInput.AccountNumber = "1234567";

        CreateFunc = Context.CreateAccountService.ExecuteAsync;
        UpdateFunc = Context.UpdateAccountService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateAccountService.ExecuteAsync;
    }

    public AccountScenario AsCurrentAccount()
    {
        return AsCurrent();
    }
}

