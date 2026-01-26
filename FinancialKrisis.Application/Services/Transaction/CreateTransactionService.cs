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
            Payee? payee = null;
            Category? category = null;
            Subcategory? subcategory = null;

            var account = (Account)ActiveEntityValidator.EnsureIsActive(await pAccountRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.AccountId));

            if (pCreateTransactionDTO.PayeeId.HasValue)
                payee = (Payee)ActiveEntityValidator.EnsureIsActive(await pPayeeRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.PayeeId.Value));

            if (pCreateTransactionDTO.CategoryId.HasValue)
                category = (Category)ActiveEntityValidator.EnsureIsActive(await pCategoryRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.CategoryId.Value));

            if (pCreateTransactionDTO.SubcategoryId.HasValue)
            {
                subcategory = (Subcategory)ActiveEntityValidator.EnsureIsActive(await pSubcategoryRepository.GetByIdOrThrowAsync(pCreateTransactionDTO.SubcategoryId.Value));

                if (category is not null && !subcategory.BelongsToCategory(category))
                    throw new ApplicationRuleException(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, typeof(Subcategory), Subcategory.Fields.Category);
            }

            Transaction transaction = new(
                account,
                pCreateTransactionDTO.Amount,
                pCreateTransactionDTO.DateTime,
                pCreateTransactionDTO.Direction,
                pCreateTransactionDTO.Memo,
                pCreateTransactionDTO.Identifier,
                payee,
                category,
                subcategory);

            await pTransactionRepository.AddAsync(transaction);

            return transaction;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
