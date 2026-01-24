using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class PayeeScenario : Scenario<PayeeScenario, CreatePayeeDTO, Payee>
{
    public PayeeScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Payee";

        CreateFunc = Context.CreatePayeeService.ExecuteAsync;
        DeactivateFunc = Context.DeactivatePayeeService.ExecuteAsync;
    }

    public PayeeScenario AsCurrentPayee()
    {
        return AsCurrent();
    }
}
