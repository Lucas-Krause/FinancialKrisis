namespace FinancialKrisis.Domain.Enums;

public enum DomainRuleErrorCode
{
    RequiredField,
    NegativeValue,
    ValueMustBePositive,
    EntityNotFound,
    WeeklyScheduleRequiresDaysOfWeek,
    MonthlyScheduleRequiresValidDayOfMonth,
}
