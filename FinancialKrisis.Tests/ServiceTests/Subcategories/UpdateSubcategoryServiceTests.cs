using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.ServiceTests.Subcategories;

public class UpdateSubcategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldUpdateNameAndCategorySuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        UpdateSubcategoryService updateSubcategoryService = scope.ServiceProvider.GetRequiredService<UpdateSubcategoryService>();

        Category category1 = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category 1" });
        Category category2 = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category 2" });
        Subcategory subcategory = await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "SubCat", CategoryId = category1.Id });

        Subcategory updatedSubcategory = await updateSubcategoryService.ExecuteAsync(new UpdateSubcategoryDTO { Id = subcategory.Id, Name = "Updated Name", CategoryId = category2.Id });

        Assert.Equal("Updated Name", updatedSubcategory.Name);
        Assert.Equal(category2.Id, updatedSubcategory.CategoryId);
        Assert.Equal("Category 2", updatedSubcategory.Category.Name);
    }

    [Fact]
    public async Task NonExistentCategory_UpdateShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        UpdateSubcategoryService updateSubcategoryService = scope.ServiceProvider.GetRequiredService<UpdateSubcategoryService>();

        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Category" });
        Subcategory subcategory = await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "SubCat", CategoryId = category.Id });
        var nonExistentCategoryId = Guid.NewGuid();

        DomainRuleException ex = await Assert.ThrowsAsync<DomainRuleException>(async () =>
        {
            await updateSubcategoryService.ExecuteAsync(new UpdateSubcategoryDTO { Id = subcategory.Id, Name = "Any", CategoryId = nonExistentCategoryId });
        });

        Assert.Equal(DomainRuleErrorCode.EntityNotFound, ex.ErrorCode);
    }
}
