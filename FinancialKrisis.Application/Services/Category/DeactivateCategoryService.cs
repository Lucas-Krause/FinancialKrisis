using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateCategoryService(ICategoryRepository pRepository)
{
    public async Task ExecuteAsync(Guid pCategoryId)
    {
        Category category = await pRepository.GetByIdOrThrowAsync(pCategoryId);
        category.Deactivate();
        await pRepository.UpdateAsync(category);
    }
}
