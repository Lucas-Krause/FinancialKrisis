using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository)
{
    public async Task<Subcategory> ExecuteAsync(CreateSubcategoryDTO pCreateSubcategoryDTO)
    {
        try
        {
            var category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pCreateSubcategoryDTO.CategoryId));
            var subcategory = new Subcategory(pCreateSubcategoryDTO.Name, category);
            await pSubcategoryRepository.AddAsync(subcategory);
            return subcategory;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
