using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Application.Services;

public abstract class UpdateFinancialMovementService<TMovement, TRepository, TUpdateDTO>(
    TRepository pMovementRepository,
    IAccountRepository pAccountRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IPayeeRepository pPayeeRepository) : UpdateEntityService<TMovement, TRepository, TUpdateDTO>(pMovementRepository)
    where TMovement : FinancialMovement, IEntity
    where TUpdateDTO : UpdateFinancialMovementDTO
    where TRepository : IFinancialMovementRepository<TMovement>
{
    protected abstract void ApplyChangesToMovement(TMovement pMovement, TUpdateDTO pDTO);

    protected override async Task ApplyChangesToEntity(TMovement pMovement, TUpdateDTO pUpdateDTO)
    {
        if (EntityRelationUpdateHelper.ShouldRemove(pUpdateDTO.AccountId))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, pMovement.GetType(), FinancialMovement.Fields.Account);

        if (EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.AccountId))
            pMovement.ChangeAccount((Account)ActiveEntityValidator.EnsureIsActive(await pAccountRepository.GetByIdOrThrowAsync(pUpdateDTO.AccountId.Value)));
        else
            ActiveEntityValidator.EnsureIsActive(await pAccountRepository.GetByIdOrThrowAsync(pMovement.AccountId));

        if (pUpdateDTO.Memo.IsDefined)
            pMovement.ChangeMemo(pUpdateDTO.Memo.Value);

        if (pUpdateDTO.Amount.IsDefined)
            pMovement.ChangeAmount(pUpdateDTO.Amount.Value);

        if (pUpdateDTO.Identifier.IsDefined)
            pMovement.ChangeIdentifier(pUpdateDTO.Identifier.Value);

        if (pUpdateDTO.Direction.IsDefined)
            pMovement.ChangeDirection(pUpdateDTO.Direction.Value);

        if (EntityRelationUpdateHelper.ShouldRemove(pUpdateDTO.PayeeId))
            pMovement.RemovePayee();

        if (EntityRelationUpdateHelper.ShouldRemove(pUpdateDTO.CategoryId))
            pMovement.RemoveCategory();

        if (EntityRelationUpdateHelper.ShouldRemove(pUpdateDTO.SubcategoryId))
            pMovement.RemoveSubcategory();

        if (EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.PayeeId))
            pMovement.ChangePayee((Payee)ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pUpdateDTO.PayeeId.Value)));

        if (EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.CategoryId))
        {
            var category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateDTO.CategoryId.Value));
            pMovement.ChangeCategory(category);

            if (pMovement.SubcategoryId.HasValue && !EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.SubcategoryId))
            {
                Subcategory currentSubcategory = await pSubcategoryRepository.GetByIdOrThrowAsync(pMovement.SubcategoryId.Value);

                if (!currentSubcategory.BelongsToCategory(category))
                    pMovement.RemoveSubcategory();
            }
        }

        if (EntityRelationUpdateHelper.ShouldAssign(pUpdateDTO.SubcategoryId))
        {
            var subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateDTO.SubcategoryId.Value));

            if (!pMovement.CategoryId.HasValue)
                pMovement.ChangeCategory((Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(subcategory.CategoryId), typeof(Subcategory)));
            else if (pMovement.CategoryId != subcategory.CategoryId)
                throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category, typeof(Transaction));

            pMovement.ChangeSubcategory(subcategory);
        }

        ApplyChangesToMovement(pMovement, pUpdateDTO);
    }
}
