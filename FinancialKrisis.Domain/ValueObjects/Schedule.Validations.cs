using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class Schedule
{
    public void Validate()
    {
        ValidateStartDate();
        ValidateInterval();

        switch (RecurrenceType)
        {
            case RecurrenceType.Weekly:
                if (DaysOfWeek is null || DaysOfWeek.Count == 0)
                    throw new DomainRuleException(DomainRuleErrorCode.WeeklyScheduleRequiresDaysOfWeek, typeof(Schedule), Fields.RecurrenceType, typeof(PlannedTransaction));
                break;

            case RecurrenceType.Monthly:
                if (DayOfMonth is null or < 1 or > 31)
                    throw new DomainRuleException(DomainRuleErrorCode.MonthlyScheduleRequiresValidDayOfMonth, typeof(Schedule), Fields.RecurrenceType, typeof(PlannedTransaction));
                break;
        }
    }

    public void ValidateStartDate()
    {
        if (StartDate == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Schedule), Fields.StartDate);
    }

    public void ValidateInterval()
    {
        if (Interval < 1)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Schedule), Fields.Interval);
    }
}
