using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class ReactivateSubcategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldReactivateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .Deactivate()
                .Reactivate()
                .ShouldReactivateSuccessfully();
    }

    [Fact]
    public void ActiveSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .Reactivate()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityAlreadyActive, typeof(Subcategory));
    }

    [Fact]
    public void NonExistentSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Subcategory()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
            .Category().Deactivate().ShouldDeactivateSuccessfully()
            .Subcategory()
                .Reactivate()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category), null, typeof(Subcategory));
    }
}
