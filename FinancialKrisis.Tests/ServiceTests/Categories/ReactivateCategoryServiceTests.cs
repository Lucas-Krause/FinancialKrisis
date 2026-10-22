using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class ReactivateCategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldReactivateSuccessfully()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .Deactivate()
            .Reactivate()
            .ShouldReactivateSuccessfully();
    }

    [Fact]
    public void ActiveCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityAlreadyActive, typeof(Category));
    }

    [Fact]
    public void NonExistentCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void SubcategoryDeactivatedByCategory_ShouldBeReactivatedTogether()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Deactivate().ShouldDeactivateSuccessfully()
            .Category().Reactivate().ShouldReactivateSuccessfully()
            .Subcategory().ShouldBeActive();
    }

    [Fact]
    public void SubcategoryDeactivatedManually_ShouldStayInactive()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().Deactivate().ShouldDeactivateSuccessfully()
            .Category().Deactivate().ShouldDeactivateSuccessfully()
            .Category().Reactivate().ShouldReactivateSuccessfully()
            .Subcategory().ShouldBeInactive();
    }
}
