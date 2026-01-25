using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class DeactivateCategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldDeactivateSuccessfully()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .Deactivate()
            .ShouldDeactivateSuccessfully();
    }

    [Fact]
    public void NonExistantCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .Deactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void CategoryWithSubcategories_ShouldDeactivateAllSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Deactivate().ShouldDeactivateSuccessfully()
            .Subcategory().ShouldBeInactive();
    }
}
