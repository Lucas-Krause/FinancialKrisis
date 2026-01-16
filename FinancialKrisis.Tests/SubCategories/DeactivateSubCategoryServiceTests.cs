using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinancialKrisis.Tests.SubCategories;

public class DeactivateSubCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivateSubCategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService pCreateCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService pCreateSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        GetSubCategoryByIdService pGetSubCategoryByIdService = scope.ServiceProvider.GetRequiredService<GetSubCategoryByIdService>();
        DeactivateSubCategoryService pDeactivateSubCategoryService = scope.ServiceProvider.GetRequiredService<DeactivateSubCategoryService>();

        Category category = await pCreateCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category" });
        SubCategory subCategory = await pCreateSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category.Id });

        await pDeactivateSubCategoryService.ExecuteAsync(subCategory.Id);

        SubCategory? subCategoryAfterDeactivation = await pGetSubCategoryByIdService.ExecuteAsync(subCategory.Id);
        Assert.NotNull(subCategoryAfterDeactivation);
        Assert.False(subCategoryAfterDeactivation.IsActive);
    }
}
