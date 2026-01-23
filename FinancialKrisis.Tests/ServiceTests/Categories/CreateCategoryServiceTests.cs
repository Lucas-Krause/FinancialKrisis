using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Helpers;
using FinancialKrisis.Tests.Scenarios;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class CreateCategoryServiceTests
{
    [Fact]
    public async Task ValidData_ShouldCreateSuccessfully()
    {
        CategoryScenario scenario = await new CategoryScenarioBuilder().WithName("Test Category").BuildAsync();

        Category createdCategory = await scenario.CreateAsync();

        Assert.Equal("Test Category", createdCategory.Name);
        Assert.True(createdCategory.IsActive);
    }

    [Fact]
    public async Task InvalidName_ShouldThrowCorrectException()
    {
        CategoryScenario scenario = await new CategoryScenarioBuilder().WithName("").BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Category>(scenario.CreateAsync, DomainRuleErrorCode.RequiredField);
    }
}
