using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Helpers;
using FinancialKrisis.Tests.Scenarios;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class UpdateCategoryServiceTests
{
    [Fact]
    public async Task ValidData_ShouldUpdateSuccessfully()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario createScenario = await scenarioBuilder.WithName("Test Category").BuildAsync();
        Category createdCategory = await createScenario.CreateAsync();

        CategoryScenario updateScenario = await scenarioBuilder.WithName("Test Category 2").BuildAsync();
        Category updatedCategory = await updateScenario.UpdateAsync(createdCategory.Id);

        Assert.Equal("Test Category 2", updatedCategory.Name);
        Assert.True(updatedCategory.IsActive);
    }

    [Fact]
    public async Task NonExistentCategory_ShouldThrowException()
    {
        CategoryScenario scenario = await new CategoryScenarioBuilder().BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Category>(() => scenario.UpdateAsync(Guid.NewGuid()), DomainRuleErrorCode.EntityNotFound);
    }

    [Fact]
    public async Task InactiveCategory_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario scenario = await scenarioBuilder.BuildAsync();
        Category createdCategory = await scenario.CreateAsync();
        await scenario.DeactivateAsync(createdCategory.Id);
        await ExceptionAssert.AssertApplicationRuleException<Category>(() => scenario.UpdateAsync(createdCategory.Id), ApplicationRuleErrorCode.EntityIsNotActive);
    }

    [Fact]
    public async Task InvalidName_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario createScenario = await scenarioBuilder.WithName("Test Category").BuildAsync();
        Category createdCategory = await createScenario.CreateAsync();
        CategoryScenario updateScenario = await scenarioBuilder.WithName("").BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Category>(() => updateScenario.UpdateAsync(createdCategory.Id), DomainRuleErrorCode.RequiredField);
    }
}
