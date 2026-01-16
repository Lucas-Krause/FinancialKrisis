using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetSubCategoryByIdService(ISubCategoryRepository pSubCategoryRepository)
{
    public async Task<SubCategory?> ExecuteAsync(Guid pSubCategoryId)
    {
        return await pSubCategoryRepository.GetByIdAsync(pSubCategoryId);
    }
}
