using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class AccountScenario : Scenario<AccountScenario, CreateAccountDTO>
{
    public override Type EntityType => typeof(Account);
    public Account? CreatedEntity { get; private set; } = null;

    public AccountScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Account";
        Input.AccountNumber = "1234567";
    }

    public AccountScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await Context.CreateAccountService.ExecuteAsync(Input));
        return this;
    }

    public AccountScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await Context.DeactivateAccountService.ExecuteAsync(Context.GetCurrentOrThrow<Account>().Id));
        return this;
    }

    public AccountScenario AsCurrentAccount()
    {
        Context.SetCurrent(CreatedEntity);
        return this;
    }
}

