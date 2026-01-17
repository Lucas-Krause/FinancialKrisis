using FinancialKrisis.Application.DTOs;
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
            transaction.UpdateDescription(pUpdateTransactionDTO.Description);
            transaction.UpdateDateTime(pUpdateTransactionDTO.DateTime);
            transaction.UpdateAmount(pUpdateTransactionDTO.Amount);

            Payee? payee = pUpdateTransactionDTO.PayeeId.HasValue
                ? await pPayeeRepository.GetByIdAsync(pUpdateTransactionDTO.PayeeId.Value)
                : null;
            transaction.UpdatePayee(pUpdateTransactionDTO.PayeeId);

            Category? category = pUpdateTransactionDTO.CategoryId.HasValue
                ? await pCategoryRepository.GetByIdAsync(pUpdateTransactionDTO.CategoryId.Value)
                : null;
            transaction.UpdateCategory(pUpdateTransactionDTO.CategoryId);

            Subcategory? subcategory = pUpdateTransactionDTO.SubcategoryId.HasValue
                ? await pSubcategoryRepository.GetByIdAsync(pUpdateTransactionDTO.SubcategoryId.Value)
                : null;
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
