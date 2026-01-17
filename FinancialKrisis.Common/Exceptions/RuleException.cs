namespace FinancialKrisis.Common.Exceptions;

public abstract class RuleException<TErrorCode>(TErrorCode pErrorCode, string pMessage) : Exception(pMessage)
    where TErrorCode : Enum
{
    public TErrorCode ErrorCode { get; } = pErrorCode;
}
