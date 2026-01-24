using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class SubcategoryScenario : Scenario<SubcategoryScenario, CreateSubcategoryDTO, Subcategory>
{
    public SubcategoryScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Subcategory";

        CreateFunc = Context.CreateSubcategoryService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateSubcategoryService.ExecuteAsync;
    }

    public SubcategoryScenario AsCurrentSubcategory()
    {
        return AsCurrent();
    }

    public SubcategoryScenario WithCurrentCategory()
    {
        Input.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }
}
