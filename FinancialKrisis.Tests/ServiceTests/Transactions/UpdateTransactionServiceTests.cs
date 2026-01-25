using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class UpdateTransactionServiceTests
{
    [Fact]
    public void ValidInput_ShouldUpdateSuccessfully()
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
                .ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Transaction()
                .UpdatingWithCurrentPayee()
                .UpdatingWithCurrentCategory()
                .UpdatingWithCurrentSubcategory()
                .UpdatingWith(UpdateInput =>
                {
                    UpdateInput.Amount = 50;
                    UpdateInput.DateTime = DateTime.Now.AddDays(2);
                    UpdateInput.Identifier = "UT1";
                    UpdateInput.Memo = "Updated Memo";
                    UpdateInput.Direction = TransactionDirection.Out;
                })
                .Update()
                .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void RemovingAllOptionalValues_ShouldUpdateSuccessfully()
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

    [Fact]
    public void InvalidDateTime_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.DateTime = Optional<DateTime>.Remove())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.DateTime);
    }

    [Fact]
    public void InvalidDirection_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.Direction = (TransactionDirection)3)
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.Direction);
    }

    [Fact]
    public void NegativeAmount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(UpdateInput => UpdateInput.Amount = -1)
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Transaction), Transaction.Fields.Amount);
    }

    [Fact]
    public void NonExistentTransaction_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Transaction()
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Transaction));
    }

    [Fact]
    public void NonExistentPayee_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.PayeeId = Guid.NewGuid())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Payee));
    }

    [Fact]
    public void NonExistentCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.CategoryId = Guid.NewGuid())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void NonExistentSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.SubcategoryId = Guid.NewGuid())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }

    [Fact]
    public void SubcategoryDoesNotBelongToCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWithCurrentCategory()
                .CreatingWithCurrentSubcategory()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Transaction()
                .UpdatingWithCurrentSubcategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
    }

    [Fact]
    public void InactiveAccount_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully()
            .Account().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .UpdatingWithCurrentCategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Fact]
    public void InactiveSubcategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .UpdatingWithCurrentSubcategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }

    [Fact]
    public void InactivePayee_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().Deactivate().ShouldDeactivateSuccessfully()
            .Transaction()
                .UpdatingWithCurrentPayee()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }
}
