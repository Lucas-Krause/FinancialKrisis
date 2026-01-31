using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public sealed class TransactionScenario : FinancialMovementScenario<TransactionScenario, CreateTransactionDTO, UpdateTransactionDTO, Transaction>
{
    public TransactionScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.DateTime = DateTime.Now;

        CreateFunc = Context.CreateTransactionService.ExecuteAsync;
        UpdateFunc = Context.UpdateTransactionService.ExecuteAsync;
    }

    public TransactionScenario AsCurrentTransaction()
    {
        return AsCurrent();
    }
}
