using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class CreateTransactionServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Payee()
                .Create()
                .AsCurrentPayee()
                .ShouldMatchInput()
            .Category()
                .Create()
                .AsCurrentCategory()
                .ShouldMatchInput()
            .Subcategory()
                .WithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .ShouldMatchInput()
            .Transaction()
                .WithCurrentAccount()
                .WithCurrentPayee()
                .WithCurrentCategory()
                .WithCurrentSubcategory()
                .Create()
                .AsCurrentTransaction()
                .ShouldMatchInput();
    }

    [Fact]
    public void NegativeAmount_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Transaction()
                .WithCurrentAccount()
                .With(input => input.Amount = -1)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeAmount);
    }

    [Fact]
    public void NonExistentAccount_ShouldThrowCorrectException()
    {
        new TestContext()
            .Transaction()
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }

    [Fact]
    public async Task SubcategoryDoesNotBelongToCategory_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Payee()
                .Create()
                .AsCurrentPayee()
                .ShouldMatchInput()
            .Category()
                .Create()
                .AsCurrentCategory()
                .ShouldMatchInput()
            .Subcategory()
                .WithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .ShouldMatchInput()
            .Category()
                .Create()
                .AsCurrentCategory()
                .ShouldMatchInput()
            .Transaction()
                .WithCurrentAccount()
                .WithCurrentPayee()
                .WithCurrentCategory()
                .WithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory));
    }

    [Fact]
    public async Task InactiveAccount_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .Deactivate()
                .ShouldBeInactive()
            .Transaction()
                .WithCurrentAccount()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Fact]
    public async Task InactivePayee_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Payee()
                .Create()
                .AsCurrentPayee()
                .Deactivate()
                .ShouldBeInactive()
            .Transaction()
                .WithCurrentAccount()
                .WithCurrentPayee()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }

    [Fact]
    public async Task InactiveCategory_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Category()
                .Create()
                .AsCurrentCategory()
                .Deactivate()
                .ShouldBeInactive()
            .Transaction()
                .WithCurrentAccount()
                .WithCurrentCategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Fact]
    public async Task InactiveSubcategory_ShouldThrowCorrectException()
    {
        new TestContext()
            .Account()
                .Create()
                .AsCurrentAccount()
                .ShouldMatchInput()
            .Category()
                .Create()
                .AsCurrentCategory()
                .ShouldMatchInput()
            .Subcategory()
                .WithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .Deactivate()
                .ShouldBeInactive()
            .Transaction()
                .WithCurrentAccount()
                .WithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }
}
