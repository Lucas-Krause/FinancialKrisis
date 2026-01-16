using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllCategoriesService(ICategoryRepository pRepository)
{
    public async Task<IReadOnlyList<Category>> ExecuteAsync()
    {
        return await pRepository.GetAllAsync();
    }
}
