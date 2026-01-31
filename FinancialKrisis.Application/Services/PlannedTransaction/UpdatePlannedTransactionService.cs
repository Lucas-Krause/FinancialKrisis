using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

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
    protected override void ApplyChangesToMovement(PlannedTransaction pPlannedTransaction, UpdatePlannedTransactionDTO pUpdateDTO)
    {
        if (pUpdateDTO.RecurrenceType.IsDefined)
            pPlannedTransaction.Schedule.ChangeRecurrenceType(pUpdateDTO.RecurrenceType.Value);

        if (pUpdateDTO.StartDate.IsDefined)
            pPlannedTransaction.Schedule.ChangeStartDate(pUpdateDTO.StartDate.Value);

        if (pUpdateDTO.EndDate.IsDefined)
            pPlannedTransaction.Schedule.ChangeEndDate(pUpdateDTO.EndDate.Value);

        if (pUpdateDTO.Interval.IsDefined)
            pPlannedTransaction.Schedule.ChangeInterval(pUpdateDTO.Interval.Value);

        if (pUpdateDTO.DaysOfWeek.IsDefined)
            pPlannedTransaction.Schedule.ChangeDaysOfWeek(pUpdateDTO.DaysOfWeek.Value!);

        if (pUpdateDTO.DayOfMonth.IsDefined)
            pPlannedTransaction.Schedule.ChangeDayOfMonth(pUpdateDTO.DayOfMonth.Value);
    }
}