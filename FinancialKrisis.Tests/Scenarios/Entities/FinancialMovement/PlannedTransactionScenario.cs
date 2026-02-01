using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public sealed class PlannedTransactionScenario : FinancialMovementScenario<PlannedTransactionScenario, CreatePlannedTransactionDTO, UpdatePlannedTransactionDTO, PlannedTransaction>
{
    public PlannedTransactionScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.StartDate = DateTime.Now.AddMonths(1);

        CreateFunc = Context.CreatePlannedTransactionService.ExecuteAsync;
        UpdateFunc = Context.UpdatePlannedTransactionService.ExecuteAsync;
    }

    public PlannedTransactionScenario AsCurrentPlannedTransaction()
    {
        return AsCurrent();
    }
}
