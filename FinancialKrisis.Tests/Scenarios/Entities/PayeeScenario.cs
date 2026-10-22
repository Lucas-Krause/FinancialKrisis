using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class PayeeScenario : Scenario<PayeeScenario, CreatePayeeDTO, UpdatePayeeDTO, Payee>
{
    public PayeeScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Name = $"Test Payee {Guid.NewGuid():N}";

        CreateFunc = Context.CreatePayeeService.ExecuteAsync;
        UpdateFunc = Context.UpdatePayeeService.ExecuteAsync;
        DeactivateFunc = Context.DeactivatePayeeService.ExecuteAsync;
        ReactivateFunc = Context.ReactivatePayeeService.ExecuteAsync;
    }

    public PayeeScenario AsCurrentPayee()
    {
        return AsCurrent();
    }
}
