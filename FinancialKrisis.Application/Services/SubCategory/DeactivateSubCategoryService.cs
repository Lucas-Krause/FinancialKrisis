using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository)
{
    public async Task ExecuteAsync(Guid pSubcategoryId)
    {
        try
        {
            Subcategory subcategory = await pSubcategoryRepository.GetByIdOrThrowAsync(pSubcategoryId);
            subcategory.Deactivate();
            await pSubcategoryRepository.UpdateAsync(subcategory);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
