using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Common.Exceptions;

public abstract class RuleException<TErrorCode>(TErrorCode pErrorCode, Type pType, FieldKey? pField, Type? pOwnerType, string pMessage) : Exception(pMessage)
    where TErrorCode : Enum
{
    public TErrorCode ErrorCode { get; } = pErrorCode;

    public Type Type { get; } = pType;

    public FieldKey? Field { get; } = pField;

    public Type? OwnerType { get; } = pOwnerType;
}
