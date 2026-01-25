using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetSubcategoryByIdService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<Subcategory?> ExecuteAsync(Guid pSubcategoryId)
    {
        try
        {
            return await pSubcategoryRepository.GetByIdAsync(pSubcategoryId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
