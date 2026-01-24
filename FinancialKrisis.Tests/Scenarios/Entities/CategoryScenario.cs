using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class CategoryScenario : Scenario<CategoryScenario, CreateCategoryDTO>
{
    public override Type EntityType => typeof(Category);
    public Category? CreatedEntity { get; private set; } = null;

    public CategoryScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Category";
    }

    public CategoryScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await Context.CreateCategoryService.ExecuteAsync(Input));
        return this;
    }

    public CategoryScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await Context.DeactivateCategoryService.ExecuteAsync(Context.GetCurrentOrThrow<Category>().Id));
        return this;
    }

    public CategoryScenario AsCurrentCategory()
    {
        Context.SetCurrent(CreatedEntity);
        return this;
    }
}
