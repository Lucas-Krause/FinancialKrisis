using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public partial class Transaction
{
    private static void ValidateDateTime(DateTime pDateTime)
    {
        if (pDateTime == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.DateTime);
    }
}
