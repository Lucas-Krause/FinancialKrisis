using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class DeactivateSubcategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldDeactivateSuccessfully()
    {
        new TestContext()
            .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
            .Subcategory()
                .CreatingWithCurrentCategory()
                .Create()
                .AsCurrentSubcategory()
                .Deactivate()
                .ShouldDeactivateSuccessfully();
    }

    [Fact]
    public void NonExistantSubcategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Subcategory()
            .Deactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Subcategory));
    }
}
