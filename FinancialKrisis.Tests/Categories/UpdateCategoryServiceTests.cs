using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Categories;

public class UpdateCategoryServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldUpdateNameSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        UpdateCategoryService updateCategoryService = scope.ServiceProvider.GetRequiredService<UpdateCategoryService>();

        Category createdCategory = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Old Name" });
        Category updatedCategory = await updateCategoryService.ExecuteAsync(new UpdateCategoryDTO { Id = createdCategory.Id, Name = "New Name" });

        Assert.Equal("New Name", updatedCategory.Name);
    }
}
