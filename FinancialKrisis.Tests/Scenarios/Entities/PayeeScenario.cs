using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class PayeeScenario : Scenario<PayeeScenario, CreatePayeeDTO>
{
    public override Type EntityType => typeof(Payee);
    public Payee? CreatedEntity { get; private set; } = null;

    public PayeeScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Payee";
    }

    public PayeeScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await Context.CreatePayeeService.ExecuteAsync(Input));
        return this;
    }

    public PayeeScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await Context.DeactivatePayeeService.ExecuteAsync(Context.GetCurrentOrThrow<Payee>().Id));
        return this;
    }

    public PayeeScenario AsCurrentPayee()
    {
        Context.SetCurrent(CreatedEntity);
        return this;
    }
}
