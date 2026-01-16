using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinancialKrisis.Tests.SubCategories;

public class CreateSubCategoryServiceTests
{
    [Fact]
    public async Task ValidCategory_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService pCreateCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubCategoryService pCreateSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();

        Category category = await pCreateCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Main Category" });
        SubCategory subCategory = await pCreateSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = category.Id });

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

        CreateSubCategoryService pCreateSubCategoryService = scope.ServiceProvider.GetRequiredService<CreateSubCategoryService>();
        Guid nonExistentCategoryId = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await pCreateSubCategoryService.ExecuteAsync(new CreateSubCategoryDTO { Name = "SubCat", CategoryId = nonExistentCategoryId });
        });
    }
}
