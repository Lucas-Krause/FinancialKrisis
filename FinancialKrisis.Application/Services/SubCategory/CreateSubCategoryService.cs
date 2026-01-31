using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository)
    : CreateEntityService<Subcategory, ISubcategoryRepository, CreateSubcategoryDTO>(pSubcategoryRepository)
{
    protected override async Task<Subcategory> CreateEntity(CreateSubcategoryDTO pCreateDTO)
    {
        var category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pCreateDTO.CategoryId));
        return new Subcategory(pCreateDTO.Name, category);
    }
}