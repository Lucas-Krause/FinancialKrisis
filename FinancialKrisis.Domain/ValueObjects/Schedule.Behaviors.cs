using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class Schedule
{
    public void ChangeRecurrenceType(RecurrenceType pRecurrenceType)
    {
        RecurrenceType = pRecurrenceType;
        Validate();
    }

    public void ChangeStartDate(DateTime pStartDate)
    {
        StartDate = pStartDate;
        Validate();
    }

    public void ChangeEndDate(DateTime pEndDate)
    {
        EndDate = pEndDate;
        Validate();
    }

    public void ChangeInterval(int pInterval)
    {
        Interval = pInterval;
        Validate();
    }

    public void ChangeDaysOfWeek(IReadOnlyCollection<DayOfWeek> pDaysOfWeek)
    {
        DaysOfWeek = pDaysOfWeek;
        Validate();
    }

    public void ChangeDayOfMonth(int pDayOfMonth)
    {
        DayOfMonth = pDayOfMonth;
        Validate();
    }
}
