using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public sealed class TransactionScenario : Scenario<TransactionScenario, CreateTransactionDTO, UpdateTransactionDTO, Transaction>
{
    public TransactionScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Identifier = "T1";
        CreateInput.Memo = "Test Transaction";
        CreateInput.DateTime = DateTime.Now;
        CreateInput.Amount = 100m;

        CreateFunc = Context.CreateTransactionService.ExecuteAsync;
        UpdateFunc = Context.UpdateTransactionService.ExecuteAsync;
    }

    public TransactionScenario AsCurrentTransaction()
    {
        return AsCurrent();
    }

    public TransactionScenario CreatingWithCurrentAccount()
    {
        CreateInput.AccountId = Context.GetCurrentOrThrow<Account>().Id;
        return this;
    }

    public TransactionScenario CreatingWithCurrentPayee()
    {
        CreateInput.PayeeId = Context.GetCurrentOrThrow<Payee>().Id;
        return this;
    }

    public TransactionScenario CreatingWithCurrentCategory()
    {
        CreateInput.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }

    public TransactionScenario CreatingWithCurrentSubcategory()
    {
        CreateInput.SubcategoryId = Context.GetCurrentOrThrow<Subcategory>().Id;
        return this;
    }

    public TransactionScenario UpdatingWithCurrentPayee()
    {
        UpdateInput.PayeeId = Context.GetCurrentOrThrow<Payee>().Id;
        return this;
    }

    public TransactionScenario UpdatingWithCurrentCategory()
    {
        UpdateInput.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }

    public TransactionScenario UpdatingWithCurrentSubcategory()
    {
        UpdateInput.SubcategoryId = Context.GetCurrentOrThrow<Subcategory>().Id;
        return this;
    }
}
