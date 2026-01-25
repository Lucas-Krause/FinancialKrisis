using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllSubCategoriesService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<IReadOnlyList<Subcategory>> ExecuteAsync()
    {
        try
        {
            return await pSubcategoryRepository.GetAllAsync();
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
