using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction
{
    private static void ValidatePlannedDateTime(DateTime pPlannedDateTime)
    {
        if (pPlannedDateTime == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(PlannedTransaction), Fields.PlannedDateTime);
    }
}
