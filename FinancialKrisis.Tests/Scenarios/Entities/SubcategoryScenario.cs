using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public class SubcategoryScenario : Scenario<SubcategoryScenario, CreateSubcategoryDTO>
{
    public override Type EntityType => typeof(Subcategory);
    public Subcategory? CreatedEntity { get; private set; } = null;

    public SubcategoryScenario(TestContext pContext) : base(pContext)
    {
        Input.Name = "Test Subcategory";
    }

    public SubcategoryScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await Context.CreateSubcategoryService.ExecuteAsync(Input));
        return this;
    }

    public SubcategoryScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await Context.DeactivateSubcategoryService.ExecuteAsync(Context.GetCurrentOrThrow<Subcategory>().Id));
        return this;
    }

    public SubcategoryScenario AsCurrentSubcategory()
    {
        Context.SetCurrent(CreatedEntity);
        return this;
    }

    public SubcategoryScenario WithCurrentCategory()
    {
        Input.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return this;
    }
}
