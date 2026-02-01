using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.ValueObjects;

public partial class Schedule
{
    public Schedule() { }

    public static class Fields
    {
        public static readonly FieldKey RecurrenceType = new("RecurrenceType");
        public static readonly FieldKey StartDate = new("StartDate");
        public static readonly FieldKey EndDate = new("EndDate");
        public static readonly FieldKey Interval = new("Interval");
        public static readonly FieldKey DaysOfWeek = new("DaysOfWeek");
        public static readonly FieldKey DayOfMonth = new("DayOfMonth");
        public static readonly FieldKey PlannedTransaction = new("PlannedTransaction");
    }

    public RecurrenceType RecurrenceType { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public int Interval { get; private set; } = 1;

    public IReadOnlyCollection<DayOfWeek>? DaysOfWeek { get; private set; }

    public int? DayOfMonth { get; private set; }
}
