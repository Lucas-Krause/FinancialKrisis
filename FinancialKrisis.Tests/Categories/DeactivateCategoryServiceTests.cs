using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Categories;

public class DeactivateCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivateCategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        GetCategoryByIdService getCategoryByIdService = scope.ServiceProvider.GetRequiredService<GetCategoryByIdService>();
        DeactivateCategoryService deactivateCategoryService = scope.ServiceProvider.GetRequiredService<DeactivateCategoryService>();

        Category createdCategory = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Test Category" });

        await deactivateCategoryService.ExecuteAsync(createdCategory.Id);

        Category? categoryAfterDeactivation = await getCategoryByIdService.ExecuteAsync(createdCategory.Id);
        Assert.NotNull(categoryAfterDeactivation);
        Assert.False(categoryAfterDeactivation.IsActive);
    }
}
