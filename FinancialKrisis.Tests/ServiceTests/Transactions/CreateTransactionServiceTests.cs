using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Helpers;
using FinancialKrisis.Tests.Scenarios;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class CreateTransactionServiceTests
{
    [Fact]
    public async Task ValidData_ShouldCreateSuccessfully()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().BuildAsync();
        Transaction transaction = await scenario.CreateAsync();

        Assert.NotNull(transaction.Identifier);
        Assert.NotNull(transaction.PayeeId);
        Assert.NotNull(transaction.CategoryId);
        Assert.NotNull(transaction.SubcategoryId);
    }

    [Fact]
    public async Task NegativeAmount_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithAmount(-1).BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Transaction>(scenario.CreateAsync, DomainRuleErrorCode.AmountIsNegative);
    }

    [Fact]
    public async Task NonExistentAccount_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithNonExistentAccount().BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Account>(scenario.CreateAsync, DomainRuleErrorCode.EntityNotFound);
    }

    [Fact]
    public async Task SubcategoryDoesNotBelongToCategory_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithSubcategoryNotBelongingToCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Subcategory>(scenario.CreateAsync, ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory);
    }

    [Fact]
    public async Task CategoryOfSubcategoryIsInactive_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithInactiveCategory().WithoutCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Category>(scenario.CreateAsync, ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactiveAccount_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithInactiveAccount().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Account>(scenario.CreateAsync, ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactiveCategory_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithInactiveCategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Category>(scenario.CreateAsync, ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactiveSubcategory_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithInactiveSubcategory().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Subcategory>(scenario.CreateAsync, ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InactivePayee_ShouldThrowCorrectException()
    {
        TransactionScenario scenario = await new TransactionScenarioBuilder().WithInactivePayee().BuildAsync();
        await ExceptionAssert.AssertApplicationRuleException<Payee>(scenario.CreateAsync, ApplicationRuleErrorCode.EntityIsNotActive);
    }
}
