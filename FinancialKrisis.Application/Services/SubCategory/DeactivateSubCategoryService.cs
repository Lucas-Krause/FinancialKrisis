using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task ExecuteAsync(Guid pSubcategoryId)
    {
        Subcategory subcategory = await pSubcategoryRepository.GetByIdOrThrowAsync(pSubcategoryId);
        subcategory.Deactivate();
        await pSubcategoryRepository.UpdateAsync(subcategory);
    }
}
