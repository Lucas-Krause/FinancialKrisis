using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubCategoryRepository pSubCategoryRepository)
{
    public async Task<Transaction> ExecuteAsync(UpdateTransactionDTO pUpdateTransactionDTO)
    {
        Transaction transaction = await pTransactionRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.Id);
        transaction.ChangeDescription(pUpdateTransactionDTO.Description);
        transaction.ChangeDateTime(pUpdateTransactionDTO.DateTime);
        transaction.ChangeAmount(pUpdateTransactionDTO.Amount);

        Account account = await pAccountRepository.GetByIdOrThrowAsync(pUpdateTransactionDTO.AccountId);
        transaction.ChangeAccount(account);

        Payee? payee = pUpdateTransactionDTO.PayeeId.HasValue 
            ? await pPayeeRepository.GetByIdAsync(pUpdateTransactionDTO.PayeeId.Value) 
            : null;
        transaction.ChangePayee(payee);

        Category? category = pUpdateTransactionDTO.CategoryId.HasValue 
            ? await pCategoryRepository.GetByIdAsync(pUpdateTransactionDTO.CategoryId.Value) 
            : null;
        transaction.ChangeCategory(category);

        SubCategory? subCategory = pUpdateTransactionDTO.SubCategoryId.HasValue 
            ? await pSubCategoryRepository.GetByIdAsync(pUpdateTransactionDTO.SubCategoryId.Value) 
            : null;
        transaction.ChangeSubCategory(subCategory);
        
        await pTransactionRepository.UpdateAsync(transaction);
        return transaction;
    }
}
