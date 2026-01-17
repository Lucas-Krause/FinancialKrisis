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
            Subcategory subcategory = await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateSubcategoryDTO.Id);
            subcategory.Rename(pUpdateSubcategoryDTO.Name);
            Category category = await pCategoryRepository.GetByIdOrThrowAsync(pUpdateSubcategoryDTO.CategoryId);
            subcategory.ChangeCategory(category);
            await pSubcategoryRepository.UpdateAsync(subcategory);
            return subcategory;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
