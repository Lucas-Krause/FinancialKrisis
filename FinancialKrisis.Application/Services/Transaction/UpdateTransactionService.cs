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
                transaction.UpdateMemo(pUpdateTransactionDTO.Memo.Value);

            if (pUpdateTransactionDTO.Amount.IsDefined)
                transaction.UpdateAmount(pUpdateTransactionDTO.Amount.Value);

            if (pUpdateTransactionDTO.Identifier.IsDefined)
                transaction.UpdateIdentifier(pUpdateTransactionDTO.Identifier.Value);

            if (pUpdateTransactionDTO.DateTime.IsDefined)
                transaction.UpdateDateTime(pUpdateTransactionDTO.DateTime.Value);

            if (pUpdateTransactionDTO.Direction.IsDefined)
                transaction.UpdateDirection(pUpdateTransactionDTO.Direction.Value);

            if (pUpdateTransactionDTO.PayeeId.IsDefined)
            {
                if (pUpdateTransactionDTO.PayeeId.Value != Guid.Empty)
                    ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.PayeeId.Value));

                transaction.UpdatePayee(pUpdateTransactionDTO.PayeeId.Value);
            }

            if (pUpdateTransactionDTO.CategoryId.IsDefined)
            {
                if (pUpdateTransactionDTO.CategoryId.Value != Guid.Empty)
                    ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.CategoryId.Value));

                transaction.UpdateCategory(pUpdateTransactionDTO.CategoryId.Value);
            }

            if (pUpdateTransactionDTO.SubcategoryId.IsDefined)
            {
                if (pUpdateTransactionDTO.SubcategoryId.Value != Guid.Empty)
                {
                    var subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.SubcategoryId.Value));

                    bool subcategoryDoesNotBelongToCategory = transaction.CategoryId.HasValue && subcategory.CategoryId != transaction.CategoryId.Value;
                    if (subcategoryDoesNotBelongToCategory)
                        throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
                }

                transaction.UpdateSubcategory(pUpdateTransactionDTO.SubcategoryId.Value);
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
