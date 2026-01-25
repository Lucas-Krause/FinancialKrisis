using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Application.Exceptions;

public class ApplicationRuleException : RuleException<ApplicationRuleErrorCode>
{
    public ApplicationRuleException(ApplicationRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pField = null)
        : base(pErrorCode, pEntityType, pField, string.Empty)
    {
    }

    public ApplicationRuleException(ApplicationRuleException pInnerApplicationRuleException, string pMessage)
        : base(pInnerApplicationRuleException.ErrorCode, pInnerApplicationRuleException.EntityType, pInnerApplicationRuleException.Field, pMessage)
    {
    }
}
