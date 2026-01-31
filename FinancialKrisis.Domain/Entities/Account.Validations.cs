using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public partial class Account
{
    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Fields.Name);
    }

    private static void ValidateAccountNumber(string pAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(pAccountNumber))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Fields.AccountNumber);
    }
    private static void ValidateInitialBalance(decimal pInitialBalance)
    {
        if (pInitialBalance < 0)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Account), Fields.InitialBalance);
    }
}
