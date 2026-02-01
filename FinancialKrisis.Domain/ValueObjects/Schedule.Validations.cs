using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class Schedule
{
    public void Validate()
    {
        ValidateStartDate();

        if (Interval < 1)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Schedule), Fields.Interval);

        switch (RecurrenceType)
        {
            case RecurrenceType.Weekly:
                if (DaysOfWeek is null || DaysOfWeek.Count == 0)
                    throw new DomainRuleException(DomainRuleErrorCode.WeeklyScheduleRequiresDaysOfWeek, typeof(Schedule), Fields.DaysOfWeek);
                break;

            case RecurrenceType.Monthly:
                if (DayOfMonth is null or < 1 or > 31)
                    throw new DomainRuleException(DomainRuleErrorCode.MonthlyScheduleRequiresValidDayOfMonth, typeof(Schedule), Fields.DayOfMonth);
                break;
        }
    }

    public void ValidateStartDate()
    {
        if (StartDate == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Schedule), Fields.StartDate);
    }
}
