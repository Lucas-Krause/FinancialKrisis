using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class DeactivateSubcategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivateSubcategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        GetSubcategoryByIdService getSubcategoryByIdService = scope.ServiceProvider.GetRequiredService<GetSubcategoryByIdService>();
        DeactivateSubcategoryService deactivateSubcategoryService = scope.ServiceProvider.GetRequiredService<DeactivateSubcategoryService>();

        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category" });
        Subcategory subcategory = await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "SubCat", CategoryId = category.Id });

        await deactivateSubcategoryService.ExecuteAsync(subcategory.Id);

        Subcategory? subcategoryAfterDeactivation = await getSubcategoryByIdService.ExecuteAsync(subcategory.Id);
        Assert.NotNull(subcategoryAfterDeactivation);
        Assert.False(subcategoryAfterDeactivation.IsActive);
    }
}
