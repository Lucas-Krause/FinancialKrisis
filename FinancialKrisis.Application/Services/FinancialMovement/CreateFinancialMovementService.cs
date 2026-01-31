using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public abstract class CreateFinancialMovementService<TMovement, TRepository, TCreateDTO>(
    TRepository pMovementRepository,
    IAccountRepository pAccountRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IPayeeRepository pPayeeRepository) : CreateEntityService<TMovement, TRepository, TCreateDTO>(pMovementRepository)
    where TMovement : FinancialMovement, IEntity
    where TCreateDTO : CreateFinancialMovementDTO
    where TRepository : IFinancialMovementRepository<TMovement>
{
    protected abstract TMovement CreateMovement(TCreateDTO pDTO, Account pAccount, Payee? pPayee, Category? pCategory, Subcategory? pSubcategory);

    protected override async Task<TMovement> CreateEntity(TCreateDTO pCreateDTO)
    {
        Payee? payee = null;
        Category? category = null;
        Subcategory? subcategory = null;

        var account = (Account)ActiveEntityValidator.EnsureIsActive(await pAccountRepository.GetByIdOrThrowAsync(pCreateDTO.AccountId));

        if (pCreateDTO.PayeeId.HasValue)
            payee = (Payee)ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pCreateDTO.PayeeId.Value));

        if (pCreateDTO.CategoryId.HasValue)
            category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pCreateDTO.CategoryId.Value));

        if (pCreateDTO.SubcategoryId.HasValue)
        {
            subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pCreateDTO.SubcategoryId.Value));

            if (category is not null && !subcategory.BelongsToCategory(category))
                throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
        }

        return CreateMovement(pCreateDTO, account, payee, category, subcategory);
    }
}
