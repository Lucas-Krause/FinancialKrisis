using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Domain.ValueObjects;

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
    protected override PlannedTransaction CreateMovement(CreatePlannedTransactionDTO pCreateDTO, Account pAccount, Payee? pPayee, Category? pCategory, Subcategory? pSubcategory)
    {
        var schedule = new PlannedSchedule(
            pCreateDTO.RecurrenceType,
            pCreateDTO.StartDate,
            pCreateDTO.EndDate,
            pCreateDTO.Interval,
            pCreateDTO.DaysOfWeek,
            pCreateDTO.DayOfMonth);

        return new(
                pAccount,
                pCreateDTO.Amount,
                pCreateDTO.Direction,
                schedule,
                pCreateDTO.Memo,
                pCreateDTO.Identifier,
                pPayee,
                pCategory,
                pSubcategory);
    }
}
