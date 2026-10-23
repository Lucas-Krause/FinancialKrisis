using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.ValueObjects;

public sealed partial record EntityName
{
    private static void Validate(string pValue, Type pOwnerType)
    {
        if (string.IsNullOrWhiteSpace(pValue))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, pOwnerType, Fields.Name);
    }
}
