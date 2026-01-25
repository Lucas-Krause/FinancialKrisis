using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Common.Exceptions;

public abstract class RuleException<TErrorCode>(TErrorCode pErrorCode, Type pEntityType, FieldKey? pField, string pMessage) : Exception(pMessage)
    where TErrorCode : Enum
{
    public TErrorCode ErrorCode { get; } = pErrorCode;

    public Type EntityType { get; } = pEntityType;

    public FieldKey? Field { get; } = pField;
}
