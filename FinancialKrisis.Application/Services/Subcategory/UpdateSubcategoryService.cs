using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

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

        bool nameOrCategoryChanged = pUpdateDTO.Name.IsDefined || EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.CategoryId);

        if (nameOrCategoryChanged && await EntityRepository.ExistsByNameAsync(pSubcategory.CategoryId, pSubcategory.Name, pSubcategory.Id))
            throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Subcategory), Subcategory.Fields.Name);
    }
}
