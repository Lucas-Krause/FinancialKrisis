using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class SubcategoryScenario : Scenario<SubcategoryScenario, CreateSubcategoryDTO, UpdateSubcategoryDTO, Subcategory>
{
    public SubcategoryScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Name = "Test Subcategory";

        CreateFunc = Context.CreateSubcategoryService.ExecuteAsync;
        DeactivateFunc = Context.DeactivateSubcategoryService.ExecuteAsync;
    }

    public SubcategoryScenario AsCurrentSubcategory()
    {
        return AsCurrent();
    }

    public SubcategoryScenario CreatingWithCurrentCategory()
    {
        CreateInput.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }
}
