using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class CategoryScenario : Scenario<CategoryScenario, CreateCategoryDTO, Category>
{
    public CategoryScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Category";

        CreateFunc = Context.CreateCategoryService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateCategoryService.ExecuteAsync;
    }

    public CategoryScenario AsCurrentCategory()
    {
        return AsCurrent();
    }
}
