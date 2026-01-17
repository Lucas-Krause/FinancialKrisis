using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetSubcategoryByIdService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<Subcategory?> ExecuteAsync(Guid pSubcategoryId)
    {
        return await pSubcategoryRepository.GetByIdAsync(pSubcategoryId);
    }
}
