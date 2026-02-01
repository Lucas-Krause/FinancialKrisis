using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.FinancialMovement;

public class UpdateFinancialMovementServiceTests
{
    public static TheoryData<Type> FinancialMovementTypes => [typeof(Transaction), typeof(PlannedTransaction)];

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void ValidInput_ShouldUpdateSuccessfully(Type pMovementType)
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
                .ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .UpdatingWithCurrentPayee()
                .UpdatingWithCurrentCategory()
                .UpdatingWithCurrentSubcategory()
                .UpdatingWith(UpdateInput =>
                {
                    UpdateInput.Amount = 50;
                    UpdateInput.Identifier = "UT1";
                    UpdateInput.Memo = "Updated Memo";
                    UpdateInput.Direction = FinancialMovementDirection.Out;
                })
                .Update()
                .ShouldUpdateSuccessfully();
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void RemovingAllOptionalValues_ShouldUpdateSuccessfully(Type pMovementType)
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
                .UpdatingWith(UpdateInput =>
                {
                    UpdateInput.Identifier = Optional<string>.Remove();
                    UpdateInput.Memo = Optional<string>.Remove();
                    UpdateInput.PayeeId = Optional<Guid>.Remove();
                    UpdateInput.CategoryId = Optional<Guid>.Remove();
                    UpdateInput.SubcategoryId = Optional<Guid>.Remove();
                })
                .Update()
                .ShouldUpdateSuccessfully();
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InvalidDirection_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .UpdatingWith(CreateInput => CreateInput.Direction = (FinancialMovementDirection)3)
                .Update()
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
                .Create()
                .AsCurrent()
                .UpdatingWith(UpdateInput => UpdateInput.Amount = -1)
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, pMovementType, Transaction.Fields.Amount);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentTransaction_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .FinancialMovement(pMovementType)
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, pMovementType);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void NonExistentPayee_ShouldFailWithDomainRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .UpdatingWith(CreateInput => CreateInput.PayeeId = Guid.NewGuid())
                .Update()
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
                .Create()
                .AsCurrent()
                .UpdatingWith(CreateInput => CreateInput.CategoryId = Guid.NewGuid())
                .Update()
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
                .Create()
                .AsCurrent()
                .UpdatingWith(CreateInput => CreateInput.SubcategoryId = Guid.NewGuid())
                .Update()
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
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .UpdatingWithCurrentSubcategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveAccount_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully()
            .Account().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveCategory_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .UpdatingWithCurrentCategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactiveSubcategory_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .UpdatingWithCurrentSubcategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }

    [Theory]
    [MemberData(nameof(FinancialMovementTypes))]
    public void InactivePayee_ShouldFailWithApplicationRuleException(Type pMovementType)
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .FinancialMovement(pMovementType)
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrent()
                .ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().Deactivate().ShouldDeactivateSuccessfully()
            .FinancialMovement(pMovementType)
                .UpdatingWithCurrentPayee()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }
}
