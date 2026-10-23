using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class CreateSubcategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .ShouldCreateSuccessfully();
    }

    [Fact]
    public void DuplicateNameInSameCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().CreatingWith(CreateInput => CreateInput.Name = "Feira").Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = " FEIRA ")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Subcategory), Subcategory.Fields.Name);
    }

    [Fact]
    public void DuplicateNameWithAccentIgnoringCaseInSameCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().CreatingWith(CreateInput => CreateInput.Name = "Farmácia").Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = "FARMÁCIA")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Subcategory), Subcategory.Fields.Name);
    }

    [Fact]
    public void SameNameInOtherCategory_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().CreatingWith(CreateInput => CreateInput.Name = "Feira").Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = "Feira")
                .Create()
                .AsCurrentSubcategory()
                .ShouldCreateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = string.Empty)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Subcategory), Subcategory.Fields.Name);
    }

    [Fact]
    public void NonExistentCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Subcategory()
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Fact]
    public void NameExceedingMaxLength_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength + 1))
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.MaxLengthExceeded, typeof(Subcategory), Subcategory.Fields.Name);
    }

    [Fact]
    public void NameWithMaxLength_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength))
                .Create()
                .ShouldCreateSuccessfully();
    }
}
