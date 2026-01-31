using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class PlannedSchedule
{
    public PlannedSchedule(
        RecurrenceType pRecurrenceType,
        DateTime pStartDate,
        DateTime? pEndDate,
        int pInterval,
        IReadOnlyCollection<DayOfWeek>? pDaysOfWeek,
        int? pDayOfMonth)
    {
        RecurrenceType = pRecurrenceType;
        StartDate = pStartDate;
        EndDate = pEndDate;
        Interval = pInterval;
        DaysOfWeek = pDaysOfWeek;
        DayOfMonth = pDayOfMonth;

        Validate();
    }
}

