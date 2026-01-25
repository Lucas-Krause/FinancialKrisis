using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateCategoryService(ICategoryRepository pCategoryRepository)
{
    public async Task ExecuteAsync(Guid pCategoryId)
    {
        try
        {
            Category category = await pCategoryRepository.GetByIdOrThrowAsync(pCategoryId);
            category.Deactivate();
            await pCategoryRepository.UpdateAsync(category);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
