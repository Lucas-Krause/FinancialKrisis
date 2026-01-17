using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.SubCategories;

public class CreateSubcategoryServiceTests
{
    [Fact]
    public async Task ValidCategory_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();

        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Main Category" });
        Subcategory subcategory = await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "SubCat", CategoryId = category.Id });

        Assert.Equal("SubCat", subcategory.Name);
        Assert.True(subcategory.IsActive);
        Assert.Equal(category.Id, subcategory.CategoryId);
        Assert.NotNull(subcategory.Category);
        Assert.Equal("Main Category", subcategory.Category.Name);
    }

    [Fact]
    public async Task NonExistentCategory_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        var nonExistentCategoryId = Guid.NewGuid();

        DomainRuleException ex = await Assert.ThrowsAsync<DomainRuleException>(async () =>
        {
            await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "SubCat", CategoryId = nonExistentCategoryId });
        });

        Assert.Equal(DomainRuleErrorCode.EntityNotFound, ex.ErrorCode);
    }
}
