using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository) : ReactivateEntityService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
    protected override async Task ValidateReactivation(Subcategory pSubcategory)
    {
        ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pSubcategory.CategoryId), typeof(Subcategory));
    }
}
