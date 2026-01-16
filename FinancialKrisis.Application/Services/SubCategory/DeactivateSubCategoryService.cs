using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubCategoryService(ISubCategoryRepository pSubCategoryRepository)
{
    public async Task ExecuteAsync(Guid pSubCategoryId)
    {
        SubCategory subCategory = await pSubCategoryRepository.GetByIdOrThrowAsync(pSubCategoryId);
        subCategory.Deactivate();
        await pSubCategoryRepository.UpdateAsync(subCategory);
    }
}
