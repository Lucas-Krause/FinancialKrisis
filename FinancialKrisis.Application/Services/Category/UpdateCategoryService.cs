using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class UpdateCategoryService(ICategoryRepository pCategoryRepository, IUnitOfWork pUnitOfWork) : UpdateEntityService<Category, ICategoryRepository, UpdateCategoryDTO>(pCategoryRepository, pUnitOfWork)
{
    protected override async Task ApplyChangesToEntity(Category pCategory, UpdateCategoryDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
        {
            pCategory.ChangeName(pUpdateDTO.Name.Value!);

            if (await EntityRepository.ExistsByNameAsync(pCategory.Name, pCategory.Id))
                throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Category), Category.Fields.Name);
        }
    }
}