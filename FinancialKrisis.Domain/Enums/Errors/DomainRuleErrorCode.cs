namespace FinancialKrisis.Domain.Enums;

public enum DomainRuleErrorCode
{
    RequiredField,
    NegativeValue,
    ValueMustBePositive,
    EntityNotFound,
    EntityAlreadyActive,
    EntityAlreadyInactive,
    WeeklyScheduleRequiresDaysOfWeek,
    MonthlyScheduleRequiresValidDayOfMonth,
}
