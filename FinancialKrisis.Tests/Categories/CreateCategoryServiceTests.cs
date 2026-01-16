using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Categories;

public class CreateCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();

        Category createdCategory = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Test Category" });

        Assert.Equal("Test Category", createdCategory.Name);
        Assert.True(createdCategory.IsActive);
    }
}
