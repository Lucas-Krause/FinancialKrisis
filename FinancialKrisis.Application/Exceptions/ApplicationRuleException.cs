using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Application.Exceptions;

public class ApplicationRuleException : RuleException<ApplicationRuleErrorCode>
{
    public Type EntityType { get; }
    public FieldKey? Field { get; }

    public ApplicationRuleException(ApplicationRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pField = null) : base(pErrorCode, string.Empty)
    {
        EntityType = pEntityType;
        Field = pField;
    }

    public ApplicationRuleException(ApplicationRuleException pInnerApplicationRuleException, string pMessage) : base(pInnerApplicationRuleException.ErrorCode, pMessage)
    {
        EntityType = pInnerApplicationRuleException.EntityType;
        Field = pInnerApplicationRuleException.Field;
    }
}
