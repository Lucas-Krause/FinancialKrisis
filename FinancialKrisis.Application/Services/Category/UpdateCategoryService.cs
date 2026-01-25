using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateCategoryService(ICategoryRepository pRepository)
{
    public async Task<Category> ExecuteAsync(UpdateCategoryDTO pUpdateCategoryDTO)
    {
        try
        {
            var category = (Category)ActiveEntityValidator.EnsureIsActive(await pRepository.GetByIdOrThrowAsync(pUpdateCategoryDTO.Id));

            if (pUpdateCategoryDTO.Name.IsDefined)
                category.ChangeName(pUpdateCategoryDTO.Name.Value!);

            await pRepository.UpdateAsync(category);
            return category;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}