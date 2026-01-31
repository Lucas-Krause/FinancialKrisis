using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class PlannedSchedule
{
    public void Validate()
    {
        if (Interval < 1)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(PlannedSchedule), Fields.Interval);

        switch (RecurrenceType)
        {
            case RecurrenceType.Weekly:
                if (DaysOfWeek is null || DaysOfWeek.Count == 0)
                    throw new DomainRuleException(DomainRuleErrorCode.WeeklyScheduleRequiresDaysOfWeek, typeof(PlannedSchedule), Fields.DaysOfWeek);
                break;

            case RecurrenceType.Monthly:
                if (DayOfMonth is null or < 1 or > 31)
                    throw new DomainRuleException(DomainRuleErrorCode.MonthlyScheduleRequiresValidDayOfMonth, typeof(PlannedSchedule), Fields.DayOfMonth);
                break;
        }
    }
}
