using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository) 
    : UpdateEntityService<Subcategory, ISubcategoryRepository, UpdateSubcategoryDTO>(pSubcategoryRepository)
{
    protected override async Task ApplyChangesToEntity(Subcategory pSubcategory, UpdateSubcategoryDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
            pSubcategory.ChangeName(pUpdateDTO.Name.Value!);

        if (EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.CategoryId))
            pSubcategory.ChangeCategory((Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateDTO.CategoryId.Value)));
    }
}
