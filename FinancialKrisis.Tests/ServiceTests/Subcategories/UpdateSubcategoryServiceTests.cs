using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class UpdateSubcategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .UpdatingWithCurrentCategory()
                .UpdatingWith(UpdateInput => UpdateInput.Name = "Updated Subcategory Name")
                .Update()
                .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .UpdatingWith(UpdateInput => UpdateInput.Name = Optional<string>.Remove())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Subcategory), Subcategory.Fields.Name);
    }

    [Fact]
    public void NonExistantSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Subcategory()
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }

    [Fact]
    public void NonExistantCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .UpdatingWith(UpdateInput => UpdateInput.CategoryId = Guid.NewGuid())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void InactiveSubcategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .Deactivate()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Subcategory));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .ShouldCreateSuccessfully()
            .Category().Create().AsCurrentCategory().Deactivate().ShouldDeactivateSuccessfully()
            .Subcategory()
                .UpdatingWithCurrentCategory()
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }
}
