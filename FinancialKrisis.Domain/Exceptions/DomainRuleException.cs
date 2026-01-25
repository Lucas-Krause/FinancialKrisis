using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Exceptions;

public class DomainRuleException : RuleException<DomainRuleErrorCode>
{
    public DomainRuleException(DomainRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pField = null)
        : base(pErrorCode, pEntityType, pField, string.Empty)
    {
    }

    public DomainRuleException(DomainRuleException pInnerDomainRuleException, string pMessage)
        : base(pInnerDomainRuleException.ErrorCode, pInnerDomainRuleException.EntityType, pInnerDomainRuleException.Field, pMessage)
    {
    }
}
