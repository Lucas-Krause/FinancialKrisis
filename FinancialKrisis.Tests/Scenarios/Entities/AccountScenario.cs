using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class AccountScenario : Scenario<AccountScenario, CreateAccountDTO, Account>
{
    public AccountScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Account";
        Input.AccountNumber = "1234567";

        CreateFunc = Context.CreateAccountService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateAccountService.ExecuteAsync;
    }

    public AccountScenario AsCurrentAccount()
    {
        return AsCurrent();
    }
}

