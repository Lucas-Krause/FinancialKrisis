using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public sealed class TransactionScenario : Scenario<TransactionScenario, CreateTransactionDTO, Transaction>
{
    public TransactionScenario(TestContext pContext) : base(pContext)
    {
        Input.Identifier = "T1";
        Input.Description = "Test Transaction";
        Input.DateTime = DateTime.Now;
        Input.Amount = 100m;

        CreateFunc = Context.CreateTransactionService.ExecuteAsync;
    }

    public TransactionScenario AsCurrentTransaction()
    {
        return AsCurrent();
    }

    public TransactionScenario WithCurrentAccount()
    {
        Input.AccountId = Context.GetCurrentOrThrow<Account>().Id;
        return this;
    }

    public TransactionScenario WithCurrentPayee()
    {
        Input.PayeeId = Context.GetCurrentOrThrow<Payee>().Id;
        return this;
    }

    public TransactionScenario WithCurrentCategory()
    {
        Input.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }

    public TransactionScenario WithCurrentSubcategory()
    {
        Input.SubcategoryId = Context.GetCurrentOrThrow<Subcategory>().Id;
        return this;
    }
}
