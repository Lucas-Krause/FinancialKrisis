using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateTransactionService(
    ITransactionRepository pTransactionRepository,
    IAccountRepository pAccountRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IPayeeRepository pPayeeRepository)
{
    public async Task<Transaction> ExecuteAsync(CreateTransactionDTO pCreateTransactionDTO)
    {
        try
        {
            await pAccountRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.AccountId);

            if (pCreateTransactionDTO.CategoryId.HasValue)
                await pCategoryRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.CategoryId.Value);

            if (pCreateTransactionDTO.SubcategoryId.HasValue)
            {
                Subcategory subcategory = await pSubcategoryRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.SubcategoryId.Value);

                bool subcategoryDoesntBelongToCategory = pCreateTransactionDTO.CategoryId.HasValue && subcategory.CategoryId != pCreateTransactionDTO.CategoryId.Value;
                if (subcategoryDoesntBelongToCategory)
                    throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
            }

            if (pCreateTransactionDTO.PayeeId.HasValue)
                await pPayeeRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.PayeeId.Value);

            Transaction transaction = new(
                pCreateTransactionDTO.AccountId,
                pCreateTransactionDTO.Direction,
                pCreateTransactionDTO.Amount,
                pCreateTransactionDTO.DateTime,
                pCreateTransactionDTO.CategoryId,
                pCreateTransactionDTO.SubcategoryId,
                pCreateTransactionDTO.PayeeId,
                pCreateTransactionDTO.Identifier,
                pCreateTransactionDTO.Description);

            await pTransactionRepository.AddAsync(transaction);

            return transaction;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
