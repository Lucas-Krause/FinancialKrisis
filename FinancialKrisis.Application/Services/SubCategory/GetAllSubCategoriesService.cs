using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllSubCategoriesService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<IReadOnlyList<Subcategory>> ExecuteAsync()
    {
        return await pSubcategoryRepository.GetAllAsync();
    }
}
