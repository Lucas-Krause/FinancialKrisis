using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.FinancialMovement;

public class CreateFinancialMovementServiceTests
{
    public static TheoryData<Type> FinancialMovementTypes => [typeof(Transaction), typeof(PlannedTransaction)];

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void ValidInput_ShouldCreateSuccessfully(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentPayee()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully();
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InvalidDirection_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.Direction = (FinancialMovementDirection)3)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, pMovementType, Transaction.Fields.Direction);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NegativeAmount_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.Amount = -1)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, pMovementType, Transaction.Fields.Amount);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentAccount_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .FinancialMovement(pMovementType)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentPayee_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.PayeeId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Payee));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentCategory_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.CategoryId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentSubcategory_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.SubcategoryId = Guid.NewGuid())
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void SubcategoryDoesNotBelongToCategory_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveAccount_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactivePayee_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentPayee()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveCategory_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveSubcategory_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentSubcategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }
}
