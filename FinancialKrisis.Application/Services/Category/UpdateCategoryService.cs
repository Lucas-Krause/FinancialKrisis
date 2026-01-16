using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateCategoryService(ICategoryRepository pRepository)
{
    public async Task<Category> ExecuteAsync(UpdateCategoryDTO pUpdateCategoryDTO)
    {
        Category category = await pRepository.GetByIdOrThrowAsync(pUpdateCategoryDTO.Id);
        category.Rename(pUpdateCategoryDTO.Name);
        await pRepository.UpdateAsync(category);
        return category;
    }
}