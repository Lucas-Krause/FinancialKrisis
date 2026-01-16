using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetCategoryByIdService(ICategoryRepository pRepository)
{
    public async Task<Category?> ExecuteAsync(Guid pCategoryId)
    {
        return await pRepository.GetByIdAsync(pCategoryId);
    }
}
