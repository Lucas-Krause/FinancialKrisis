using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Application.Exceptions;

public class ApplicationRuleException : RuleException<ApplicationRuleErrorCode>
{
    public ApplicationRuleException(ApplicationRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pField = null, Type? pOwnerType = null)
        : base(pErrorCode, pEntityType, pField, pOwnerType, string.Empty)
    {
    }

    public ApplicationRuleException(ApplicationRuleException pInnerApplicationRuleException, string pMessage)
        : base(pInnerApplicationRuleException.ErrorCode, pInnerApplicationRuleException.Type, pInnerApplicationRuleException.Field, pInnerApplicationRuleException.OwnerType, pMessage)
    {
    }
}
