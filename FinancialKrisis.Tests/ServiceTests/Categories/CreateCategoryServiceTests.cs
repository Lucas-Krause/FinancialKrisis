using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class CreateCategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .ShouldCreateSuccessfully();
    }

    [Fact]
    public void DuplicateNameIgnoringCaseAndSpaces_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().CreatingWith(CreateInput => CreateInput.Name = "Mercado").Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Category()
                .CreatingWith(CreateInput => CreateInput.Name = " MERCADO  ")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Category), Category.Fields.Name);
    }

    [Fact]
    public void DuplicateNameWithAccentIgnoringCase_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().CreatingWith(CreateInput => CreateInput.Name = "Saúde").Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Category()
                .CreatingWith(CreateInput => CreateInput.Name = "SAÚDE")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Category), Category.Fields.Name);
    }

    [Fact]
    public void SameNameWithoutAccent_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category().CreatingWith(CreateInput => CreateInput.Name = "Café").Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Category()
                .CreatingWith(CreateInput => CreateInput.Name = "Cafe")
                .Create()
                .ShouldCreateSuccessfully();
    }

    [Fact]
    public void DuplicateNameOfInactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().CreatingWith(CreateInput => CreateInput.Name = "Mercado").Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Category()
                .CreatingWith(CreateInput => CreateInput.Name = "Mercado")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Category), Category.Fields.Name);
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .CreatingWith(CreateInput => CreateInput.Name = string.Empty)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Category), Category.Fields.Name);
    }

    [Fact]
    public void NameExceedingMaxLength_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength + 1))
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.MaxLengthExceeded, typeof(Category), Category.Fields.Name);
    }

    [Fact]
    public void NameWithMaxLength_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category()
            .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength))
            .Create()
            .ShouldCreateSuccessfully();
    }
}
