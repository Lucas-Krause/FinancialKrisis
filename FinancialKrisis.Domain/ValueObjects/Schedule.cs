using FinancialKrisis.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Domain.ValueObjects;

[Owned]
public partial class Schedule
{
    public Schedule(
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

