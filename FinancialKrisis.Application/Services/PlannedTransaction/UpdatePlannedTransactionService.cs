using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdatePlannedTransactionService(
    IPlannedTransactionRepository pPlannedTransactionRepository,
    IAccountRepository pAccountRepository,
    IPayeeRepository pPayeeRepository,
    ICategoryRepository pCategoryRepository,
    ISubcategoryRepository pSubcategoryRepository)
    : UpdateFinancialMovementService<PlannedTransaction, IPlannedTransactionRepository, UpdatePlannedTransactionDTO>(
        pPlannedTransactionRepository,
        pAccountRepository,
        pCategoryRepository,
        pSubcategoryRepository,
        pPayeeRepository)
{
    protected override void ApplyChangesToMovement(PlannedTransaction pPlannedTransaction, UpdatePlannedTransactionDTO pUpdatePlannedTransactionDTO)
    {
        if (pUpdatePlannedTransactionDTO.PlannedDateTime.IsDefined)
            pPlannedTransaction.ChangePlannedDateTime(pUpdatePlannedTransactionDTO.PlannedDateTime.Value);
    }
}