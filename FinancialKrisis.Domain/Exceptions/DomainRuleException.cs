using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Exceptions;

public class DomainRuleException : RuleException<DomainRuleErrorCode>
{
    public DomainRuleException(DomainRuleErrorCode pErrorCode, Type pType, FieldKey? pField = null, Type? pOwnerType = null)
        : base(pErrorCode, pType, pField, pOwnerType, string.Empty)
    {
    }

    public DomainRuleException(DomainRuleException pInnerDomainRuleException, string pMessage)
        : base(pInnerDomainRuleException.ErrorCode, pInnerDomainRuleException.Type, pInnerDomainRuleException.Field, pInnerDomainRuleException.OwnerType, pMessage)
    {
    }
}
