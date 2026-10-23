using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository, IUnitOfWork pUnitOfWork) : ReactivateEntityService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository, pUnitOfWork)
{
    protected override async Task ValidateReactivation(Subcategory pSubcategory)
    {
        ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pSubcategory.CategoryId), typeof(Subcategory));
    }
}
