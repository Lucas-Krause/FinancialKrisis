using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Abstractions;

public abstract partial class FinancialMovement
{
    protected void ValidateAmount(decimal pAmount)
    {
        if (pAmount < 0)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, GetType(), Fields.Amount);
    }

    protected void ValidateDirection(FinancialMovementDirection pDirection)
    {
        if (!Enum.IsDefined(pDirection))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, GetType(), Fields.Direction);
    }
}
