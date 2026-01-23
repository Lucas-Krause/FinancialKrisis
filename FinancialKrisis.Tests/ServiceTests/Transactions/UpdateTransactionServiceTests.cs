using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Helpers;
using FinancialKrisis.Tests.Scenarios;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class UpdateTransactionServiceTests
{
    [Fact]
    public async Task ValidData_ShouldUpdateSuccessfully()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();

        Guid originalTransactionId = transaction.Id;
        Guid originalAccountId = transaction.AccountId;
        Guid? originalPayeeId = transaction.PayeeId;
        Guid? originalCategoryId = transaction.CategoryId;
        Guid? originalSubcategoryId = transaction.SubcategoryId;
        DateTime originalDateTime = transaction.DateTime;
        decimal originalAmount = transaction.Amount;
        string? originalIdentifier = transaction.Identifier;
        string? originalDescription = transaction.Description;

        TransactionScenario updateScenario = await scenarioBuilder
            .WithAmount(123)
            .WithIdentifier("T2")
            .WithDescription("Updated Transaction")
            .BuildAsync();
        Transaction updatedTransaction = await updateScenario.UpdateAsync(transaction.Id);

        Assert.Equal(originalTransactionId, updatedTransaction.Id);
        Assert.Equal(originalAccountId, updatedTransaction.AccountId);
        Assert.NotEqual(originalPayeeId, updatedTransaction.PayeeId);
        Assert.NotEqual(originalCategoryId, updatedTransaction.CategoryId);
        Assert.NotEqual(originalSubcategoryId, updatedTransaction.SubcategoryId);
        Assert.NotEqual(originalDateTime, updatedTransaction.DateTime);
        Assert.NotEqual(originalAmount, updatedTransaction.Amount);
        Assert.NotEqual(originalIdentifier, updatedTransaction.Identifier);
        Assert.NotEqual(originalDescription, updatedTransaction.Description);
    }

    [Fact]
    public async Task RemovingAllOptionalRelations_ShouldUpdateSuccessfully()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();

        Guid originalTransactionId = transaction.Id;
        Guid originalAccountId = transaction.AccountId;

        Assert.NotNull(transaction.PayeeId);
        Assert.NotNull(transaction.CategoryId);
        Assert.NotNull(transaction.SubcategoryId);

        TransactionScenario updateScenario = await scenarioBuilder
            .WithoutPayee()
            .WithoutCategory()
            .WithoutSubcategory()
            .BuildAsync();
        Transaction updatedTransaction = await updateScenario.UpdateAsync(transaction.Id);

        Assert.Equal(originalTransactionId, updatedTransaction.Id);
        Assert.Equal(originalAccountId, updatedTransaction.AccountId);
        Assert.Null(updatedTransaction.PayeeId);
        Assert.Null(updatedTransaction.CategoryId);
        Assert.Null(updatedTransaction.SubcategoryId);
    }

    [Fact]
    public async Task NonExistentTransaction_ShouldThrowException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Transaction>(() => scenario.UpdateAsync(Guid.NewGuid()), DomainRuleErrorCode.EntityNotFound);
    }

    [Fact]
    public async Task SubcategoryDoesNotBelongToCategory_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();
        TransactionScenario updateScenario = await scenarioBuilder.WithSubcategoryNotBelongingToCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Subcategory>(() => updateScenario.UpdateAsync(transaction.Id), ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory);
    }

    [Fact]
    public async Task CategoryOfSubcategoryIsInactive_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();
        TransactionScenario updateScenario = await scenarioBuilder.WithInactiveCategory().WithoutCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Category>(() => updateScenario.UpdateAsync(transaction.Id), ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactiveCategory_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();
        TransactionScenario updateScenario = await scenarioBuilder.WithInactiveCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Category>(() => updateScenario.UpdateAsync(transaction.Id), ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactiveSubcategory_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();
        TransactionScenario updateScenario = await scenarioBuilder.WithInactiveSubcategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Subcategory>(() => updateScenario.UpdateAsync(transaction.Id), ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactivePayee_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new TransactionScenarioBuilder();

        TransactionScenario createScenario = await scenarioBuilder.BuildAsync();
        Transaction transaction = await createScenario.CreateAsync();
        TransactionScenario updateScenario = await scenarioBuilder.WithInactivePayee().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Payee>(() => updateScenario.UpdateAsync(transaction.Id), ApplicationRuleErrorCode.EntityIsNotActive);
    }
}
