using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Domain.Exceptions;

public class DomainRuleException : RuleException<DomainRuleErrorCode>
{
    public Type EntityType { get; }
    public FieldKey? Field { get; }

    public DomainRuleException(DomainRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pField = null) : base(pErrorCode, string.Empty)
    {
        EntityType = pEntityType;
        Field = pField;
    }

    public DomainRuleException(DomainRuleException pInnerDomainRuleException, string pMessage) : base(pInnerDomainRuleException.ErrorCode, pMessage)
    {
        EntityType = pInnerDomainRuleException.EntityType;
        Field = pInnerDomainRuleException.Field;
    }
}
