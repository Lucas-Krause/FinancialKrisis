using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public sealed class TransactionScenario : Scenario<TransactionScenario, CreateTransactionDTO>
{
    public override Type EntityType => typeof(Transaction);
    public Transaction? CreatedEntity { get; private set; } = null;

    public TransactionScenario(TestContext pContext) : base(pContext)
    {
        Input.Identifier = "T1";
        Input.Description = "Test Transaction";
        Input.DateTime = DateTime.Now;
        Input.Amount = 100m;
    }

    public TransactionScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await Context.CreateTransactionService.ExecuteAsync(Input));
        return this;
    }

    public TransactionScenario Delete()
    {
        ExecuteScenarioResultSync(async () => await Context.DeleteTransactionService.ExecuteAsync(Context.GetCurrentOrThrow<Transaction>().Id));
        return this;
    }

    public TransactionScenario AsCurrentTransaction()
    {
        Context.SetCurrent(CreatedEntity);
        return this;
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
