using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetSubcategoriesByCategoryIdService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<IReadOnlyList<Subcategory>> ExecuteAsync(Guid pCategoryId)
    {
        try
        {
            return await pSubcategoryRepository.GetByCategoryIdAsync(pCategoryId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
