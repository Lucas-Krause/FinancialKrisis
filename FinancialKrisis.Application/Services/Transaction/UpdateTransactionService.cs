using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<Transaction> ExecuteAsync(UpdateTransactionDTO pUpdateTransactionDTO)
    {
        try
        {
            Transaction transaction = await pTransactionRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.Id);

            ActiveEntityValidator.EnsureIsActive(await pAccountRepository.GetByIdOrThrowAsync(transaction.AccountId));

            if (pUpdateTransactionDTO.Memo.IsDefined)
                transaction.ChangeMemo(pUpdateTransactionDTO.Memo.Value);

            if (pUpdateTransactionDTO.Amount.IsDefined)
                transaction.ChangeAmount(pUpdateTransactionDTO.Amount.Value);

            if (pUpdateTransactionDTO.Identifier.IsDefined)
                transaction.ChangeIdentifier(pUpdateTransactionDTO.Identifier.Value);

            if (pUpdateTransactionDTO.DateTime.IsDefined)
                transaction.ChangeDateTime(pUpdateTransactionDTO.DateTime.Value);

            if (pUpdateTransactionDTO.Direction.IsDefined)
                transaction.ChangeDirection(pUpdateTransactionDTO.Direction.Value);

            if (EntityRelationUpdateHelper.ShouldRemove(pUpdateTransactionDTO.PayeeId))
                transaction.RemovePayee();

            if (EntityRelationUpdateHelper.ShouldRemove(pUpdateTransactionDTO.CategoryId))
                transaction.RemoveCategory();

            if (EntityRelationUpdateHelper.ShouldRemove(pUpdateTransactionDTO.SubcategoryId))
                transaction.RemoveSubcategory();

            if (EntityRelationUpdateHelper.ShouldAssign(pUpdateTransactionDTO.PayeeId))
                transaction.ChangePayee((Payee)ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.PayeeId.Value)));

            if (EntityRelationUpdateHelper.ShouldAssign(pUpdateTransactionDTO.CategoryId))
                transaction.ChangeCategory((Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.CategoryId.Value)));

            if (EntityRelationUpdateHelper.ShouldAssign(pUpdateTransactionDTO.SubcategoryId))
            {
                var subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.SubcategoryId.Value));

                if (transaction.CategoryId.HasValue && !subcategory.BelongsToCategory(transaction.Category!))
                    throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);

                transaction.ChangeSubcategory(subcategory);
            }

            await pTransactionRepository.UpdateAsync(transaction);
            return transaction;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
