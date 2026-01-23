using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateTransactionService(
    ITransactionRepository pTransactionRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository)
{
    public async Task<Transaction> ExecuteAsync(UpdateTransactionDTO pUpdateTransactionDTO)
    {
        try
        {
            Transaction transaction = await pTransactionRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.Id);
            transaction.UpdateIdentifier(pUpdateTransactionDTO.Identifier);
            transaction.UpdateDescription(pUpdateTransactionDTO.Description);
            transaction.UpdateDateTime(pUpdateTransactionDTO.DateTime);
            transaction.UpdateAmount(pUpdateTransactionDTO.Amount);

            if (pUpdateTransactionDTO.PayeeId.HasValue)
                ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.PayeeId.Value));

            transaction.UpdatePayee(pUpdateTransactionDTO.PayeeId);

            if (pUpdateTransactionDTO.CategoryId.HasValue)
                ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.CategoryId.Value));

            transaction.UpdateCategory(pUpdateTransactionDTO.CategoryId);

            if (pUpdateTransactionDTO.SubcategoryId.HasValue)
            {
                var subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.SubcategoryId.Value));

                if (!pUpdateTransactionDTO.CategoryId.HasValue)
                    ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(subcategory.CategoryId));

                bool subcategoryDoesNotBelongToCategory = pUpdateTransactionDTO.CategoryId.HasValue && subcategory.CategoryId != pUpdateTransactionDTO.CategoryId.Value;
                if (subcategoryDoesNotBelongToCategory)
                    throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
            }

            transaction.UpdateSubcategory(pUpdateTransactionDTO.SubcategoryId);

            await pTransactionRepository.UpdateAsync(transaction);
            return transaction;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
