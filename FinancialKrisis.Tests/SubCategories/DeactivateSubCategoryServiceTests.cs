using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.SubCategories;

public class DeactivateSubCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivateSubCategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService createSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        GetSubCategoryByIdService getSubCategoryByIdService = scope.ServiceProvider.GetRequiredService<GetSubCategoryByIdService>();
        DeactivateSubCategoryService deactivateSubCategoryService = scope.ServiceProvider.GetRequiredService<DeactivateSubCategoryService>();

        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category" });
        SubCategory subCategory = await createSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category.Id });

        await deactivateSubCategoryService.ExecuteAsync(subCategory.Id);

        SubCategory? subCategoryAfterDeactivation = await getSubCategoryByIdService.ExecuteAsync(subCategory.Id);
        Assert.NotNull(subCategoryAfterDeactivation);
        Assert.False(subCategoryAfterDeactivation.IsActive);
    }
}
