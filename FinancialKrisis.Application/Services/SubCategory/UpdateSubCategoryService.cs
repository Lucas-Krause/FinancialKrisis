using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository)
{
    public async Task<Subcategory> ExecuteAsync(UpdateSubcategoryDTO pUpdateSubcategoryDTO)
    {
        try
        {
            var subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateSubcategoryDTO.Id));

            if (pUpdateSubcategoryDTO.Name.IsDefined)
                subcategory.ChangeName(pUpdateSubcategoryDTO.Name.Value!);

            if (EntityRelationUpdateHelper.ShouldAssign(pUpdateSubcategoryDTO.CategoryId))
                subcategory.ChangeCategory((Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateSubcategoryDTO.CategoryId.Value)));

            await pSubcategoryRepository.UpdateAsync(subcategory);
            return subcategory;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
