using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateSubCategoryService(ISubCategoryRepository pSubCategoryRepository, ICategoryRepository pCategoryRepository)
{
    public async Task<SubCategory> ExecuteAsync(CreateSubCategoryDTO pCreateSubCategoryDTO)
    {
        Category category = await pCategoryRepository.GetByIdOrThrowAsync(pCreateSubCategoryDTO.CategoryId);
        var subCategory = new SubCategory(pCreateSubCategoryDTO.Name, category);
        await pSubCategoryRepository.AddAsync(subCategory);
        return subCategory;
    }
}
