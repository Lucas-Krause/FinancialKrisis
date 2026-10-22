using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, ICategoryRepository pCategoryRepository)
    : CreateEntityService<Subcategory, ISubcategoryRepository, CreateSubcategoryDTO>(pSubcategoryRepository)
{
    protected override async Task<Subcategory> CreateEntity(CreateSubcategoryDTO pCreateDTO)
    {
        var category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pCreateDTO.CategoryId));
        var subcategory = new Subcategory(pCreateDTO.Name, category);

        if (await EntityRepository.ExistsByNameAsync(category.Id, subcategory.Name))
            throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Subcategory), Subcategory.Fields.Name);

        return subcategory;
    }
}