using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllSubCategoriesService(ISubCategoryRepository pSubCategoryRepository)
{
    public async Task<IReadOnlyList<SubCategory>> ExecuteAsync()
    {
        return await pSubCategoryRepository.GetAllAsync();
    }
}
