using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class CategoryScenarioBuilder : BaseScenarioBuilder<CategoryScenario>
{
    private readonly List<CreateSubcategoryDTO> _subcaregoryDTOs = [];
    private string _name = "Test Category";

    protected override async Task<CategoryScenario> BuildInternalAsync()
    {
        return new CategoryScenario(Scope, _name, _subcaregoryDTOs);
    }

    public CategoryScenarioBuilder WithName(string pName)
    {
        _name = pName;
        return this;
    }

    public CategoryScenarioBuilder WithSubcategory(string pSubcategoryName)
    {
        var subcategoryDTO = new CreateSubcategoryDTO
        {
            Name = pSubcategoryName
        };

        _subcaregoryDTOs.Add(subcategoryDTO);
        return this;
    }
}
