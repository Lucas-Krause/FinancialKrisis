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
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentPayee()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully();
    }

    [Fact]
    public void InvalidDateTime_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.DateTime = default)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.DateTime);
    }

    [Fact]
    public void InvalidDirection_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.Direction = (FinancialMovementDirection)3)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.Direction);
    }

    [Fact]
    public void NegativeAmount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.Amount = -1)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Transaction), Transaction.Fields.Amount);
    }

    [Fact]
    public void NonExistentAccount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Transaction()
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }

    [Fact]
    public void NonExistentPayee_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.PayeeId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Payee));
    }

    [Fact]
    public void NonExistentCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.CategoryId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void NonExistentSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.SubcategoryId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }

    [Fact]
    public void SubcategoryDoesNotBelongToCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
    }

    [Fact]
    public void InactiveAccount_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Fact]
    public void InactivePayee_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentPayee()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Fact]
    public void InactiveSubcategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }
}
