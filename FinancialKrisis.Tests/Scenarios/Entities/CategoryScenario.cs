using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class CategoryScenario : Scenario<CategoryScenario, CreateCategoryDTO, UpdateCategoryDTO, Category>
{
    public CategoryScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Name = "Test Category";

        CreateFunc = Context.CreateCategoryService.ExecuteAsync;
        UpdateFunc = Context.UpdateCategoryService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateCategoryService.ExecuteAsync;
        ReactivateFunc = Context.ReactivateCategoryService.ExecuteAsync;
    }

    public CategoryScenario AsCurrentCategory()
    {
        return AsCurrent();
    }
}
