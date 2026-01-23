using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Helpers;
using FinancialKrisis.Tests.Scenarios;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class DeactivateCategoryServiceTests
{
    [Fact]
    public async Task ValidId_ShouldDeactivateSuccessfully()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario scenario = await scenarioBuilder.BuildAsync();
        Category createdCategory = await scenario.CreateAsync();
        await scenario.DeactivateAsync(createdCategory.Id);
        Category? deactivatedCategory = await scenario.GetCategoryById(createdCategory.Id);
        Assert.NotNull(deactivatedCategory);
        Assert.False(deactivatedCategory!.IsActive);
    }

    [Fact]
    public async Task InvalidId_ShouldThrowCorrectException()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario scenario = await scenarioBuilder.BuildAsync();
        await ExceptionAssert.AssertDomainRuleException<Category>(() => scenario.DeactivateAsync(Guid.NewGuid()), DomainRuleErrorCode.EntityNotFound);
    }

    [Fact]
    public async Task CategoryWithSubcategories_ShouldDeactivateAllSuccessfully()
    {
        var scenarioBuilder = new CategoryScenarioBuilder();

        CategoryScenario scenario = await scenarioBuilder.WithSubcategory("Test Subcategory 1").WithSubcategory("Test Subcategory 2").BuildAsync();
        Category createdCategory = await scenario.CreateAsync();
        await scenario.DeactivateAsync(createdCategory.Id);
        Category? deactivatedCategory = await scenario.GetCategoryById(createdCategory.Id);
        Assert.NotNull(deactivatedCategory);
        Assert.False(deactivatedCategory!.IsActive);
        IReadOnlyList<Subcategory> deactivatedSubcategories = await scenario.GetSubcategoriesByCategoryId(createdCategory.Id);
        Assert.All(deactivatedSubcategories, subcategory => Assert.False(subcategory.IsActive));
    }
}
