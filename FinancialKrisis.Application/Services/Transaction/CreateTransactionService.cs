using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubCategoryRepository pSubCategoryRepository)
{
    public async Task<Transaction> ExecuteAsync(CreateTransactionDTO pCreateTransactionDTO)
    {
        Account account = await pAccountRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.AccountId);
        Payee? payee = pCreateTransactionDTO.PayeeId.HasValue ? await pPayeeRepository.GetByIdAsync(pCreateTransactionDTO.PayeeId.Value) : null;
        Category? category = pCreateTransactionDTO.CategoryId.HasValue ? await pCategoryRepository.GetByIdAsync(pCreateTransactionDTO.CategoryId.Value) : null;
        SubCategory? subCategory = pCreateTransactionDTO.SubCategoryId.HasValue ? await pSubCategoryRepository.GetByIdAsync(pCreateTransactionDTO.SubCategoryId.Value) : null;

        var transaction = new Transaction(
            pCreateTransactionDTO.Identifier,
            pCreateTransactionDTO.Description,
            pCreateTransactionDTO.DateTime,
            account,
            payee,
            category,
            subCategory,
            pCreateTransactionDTO.Amount);

        await pTransactionRepository.AddAsync(transaction);
        return transaction;
    }
}
