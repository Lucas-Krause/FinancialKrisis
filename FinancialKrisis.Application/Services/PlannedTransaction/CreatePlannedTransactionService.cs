using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreatePlannedTransactionService(
    IPlannedTransactionRepository pPlannedTransactionRepository,
    IAccountRepository pAccountRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository,
    IPayeeRepository pPayeeRepository)
    : CreateFinancialMovementService<PlannedTransaction, IPlannedTransactionRepository, CreatePlannedTransactionDTO>(
        pPlannedTransactionRepository,
        pAccountRepository,
        pCategoryRepository,
        pSubcategoryRepository,
        pPayeeRepository)
{
    protected override PlannedTransaction CreateMovement(CreatePlannedTransactionDTO pDTO, Account pAccount, Payee? pPayee, Category? pCategory, Subcategory? pSubcategory)
    {
        return new(
                pAccount,
                pDTO.Amount,
                pDTO.Direction,
                pDTO.PlannedDateTime,
                pDTO.Memo,
                pDTO.Identifier,
                pPayee,
                pCategory,
                pSubcategory);
    }
}
