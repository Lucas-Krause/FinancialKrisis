using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreateCategoryService(ICategoryRepository pCategoryRepository) : CreateEntityService<Category, ICategoryRepository, CreateCategoryDTO>(pCategoryRepository)
{
    protected override async Task<Category> CreateEntity(CreateCategoryDTO pCreateDTO)
    {
        var category = new Category(pCreateDTO.Name);

        if (await EntityRepository.ExistsByNameAsync(category.Name))
            throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Category), Category.Fields.Name);

        return category;
    }
}