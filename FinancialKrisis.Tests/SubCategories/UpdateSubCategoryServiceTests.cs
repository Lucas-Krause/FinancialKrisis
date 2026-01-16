using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.SubCategories;

public class UpdateSubCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldUpdateNameAndCategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService pCreateCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService pCreateSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        UpdateSubCategoryService pUpdateSubCategoryService = scope.ServiceProvider.GetRequiredService<UpdateSubCategoryService>();

        Category category1 = await pCreateCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category 1" });
        Category category2 = await pCreateCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category 2" });
        SubCategory subCategory = await pCreateSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category1.Id });

        SubCategory updatedSubCategory = await pUpdateSubCategoryService.ExecuteAsync(new UpdateSubCategoryDTO { Id = subCategory.Id, Name = "Updated Name", CategoryId = category2.Id });

        Assert.Equal("Updated Name", updatedSubCategory.Name);
        Assert.Equal(category2.Id, updatedSubCategory.CategoryId);
        Assert.Equal("Category 2", updatedSubCategory.Category.Name);
    }

    [Fact]
    public async Task NonExistentCategory_UpdateShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService pCreateCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService pCreateSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        UpdateSubCategoryService pUpdateSubCategoryService = scope.ServiceProvider.GetRequiredService<UpdateSubCategoryService>();

        Category category = await pCreateCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category" });
        SubCategory subCategory = await pCreateSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category.Id });
        Guid nonExistentCategoryId = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await pUpdateSubCategoryService.ExecuteAsync(new UpdateSubCategoryDTO { Id = subCategory.Id, Name = "Any", CategoryId = nonExistentCategoryId });
        });
    }
}
