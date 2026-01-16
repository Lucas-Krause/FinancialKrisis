using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateSubCategoryService(ISubCategoryRepository pSubCategoryRepository, ICategoryRepository pCategoryRepository)
{
    public async Task<SubCategory> ExecuteAsync(UpdateSubCategoryDTO pUpdateSubCategoryDTO)
    {
        SubCategory subCategory = await pSubCategoryRepository.GetByIdOrThrowAsync(pUpdateSubCategoryDTO.Id);
        subCategory.Rename(pUpdateSubCategoryDTO.Name);
        Category category = await pCategoryRepository.GetByIdOrThrowAsync(pUpdateSubCategoryDTO.CategoryId);
        subCategory.ChangeCategory(category);
        await pSubCategoryRepository.UpdateAsync(subCategory);
        return subCategory;
    }
}
