using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.SubCategories;

public class CreateSubCategoryServiceTests
{
    [Fact]
    public async Task ValidCategory_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService createSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();

        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Main Category" });
        SubCategory subCategory = await createSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category.Id });

        Assert.Equal("SubCat", subCategory.Name);
        Assert.True(subCategory.IsActive);
        Assert.Equal(category.Id, subCategory.CategoryId);
        Assert.NotNull(subCategory.Category);
        Assert.Equal("Main Category", subCategory.Category.Name);
    }

    [Fact]
    public async Task NonExistentCategory_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateSubCategoryService createSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        var nonExistentCategoryId = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await createSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = nonExistentCategoryId });
        });
    }
}
